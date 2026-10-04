using Android.Util;
using Android.Widget;
using AndroidX.Core.Widget;
using Plugin.Maui.FitText.Controls;

namespace Plugin.Maui.FitText;

public partial class FitTextLabelHandler
{
    (int Min, int Max)? _appliedRange;

    partial void UpdateFitText(FitTextLabel label) =>
        AutoSizeText.Apply(PlatformView, label.MinFontSize, label.MaxFontSize, FitTextRange.Lines(label), ref _appliedRange);
}

public partial class FitTextButtonHandler
{
    (int Min, int Max)? _appliedRange;

    partial void UpdateFitText(FitTextButton button) =>
        AutoSizeText.Apply(PlatformView, button.MinFontSize, button.MaxFontSize, 1, ref _appliedRange);
}

static class AutoSizeText
{
    public static void Apply(TextView textView, double minFontSize, double maxFontSize, int lines, ref (int Min, int Max)? appliedRange)
    {
        // The autosize function does not change the text size of a view that scrolls horizontally
        textView.SetHorizontallyScrolling(false);

        if (textView.MaxLines != lines)
            textView.SetMaxLines(lines);

        var (min, max) = FitTextRange.Normalize(minFontSize, maxFontSize);
        (int Min, int Max) range = ((int)Math.Round(min), (int)Math.Round(max));

        if (appliedRange == range)
            return;

        appliedRange = range;

        // TextViewCompat also operates on API levels less than 26, where TextView does not have the autosize function
        if (range.Max > range.Min)
        {
            TextViewCompat.SetAutoSizeTextTypeUniformWithConfiguration(textView, range.Min, range.Max, 1, (int)ComplexUnitType.Sp);
        }
        else
        {
            // Android does not accept a range where the minimum and the maximum are equal
            TextViewCompat.SetAutoSizeTextTypeWithDefaults(textView, TextViewCompat.AutoSizeTextTypeNone);
            textView.SetTextSize(ComplexUnitType.Sp, range.Min);
        }
    }
}
