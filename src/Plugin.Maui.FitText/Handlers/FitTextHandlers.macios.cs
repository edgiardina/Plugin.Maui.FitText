using Plugin.Maui.FitText.Controls;
using UIKit;

namespace Plugin.Maui.FitText;

public partial class FitTextLabelHandler
{
    partial void UpdateFitText(FitTextLabel label) =>
        AdjustsFontSize.Apply(PlatformView, label.MinFontSize, label.MaxFontSize, FitTextRange.Lines(label));
}

public partial class FitTextButtonHandler
{
    partial void UpdateFitText(FitTextButton button)
    {
        if (PlatformView.TitleLabel is UILabel titleLabel)
            AdjustsFontSize.Apply(titleLabel, button.MinFontSize, button.MaxFontSize, 1);
    }
}

static class AdjustsFontSize
{
    public static void Apply(UILabel label, double minFontSize, double maxFontSize, int lines)
    {
        var (min, max) = FitTextRange.Normalize(minFontSize, maxFontSize);

        label.AdjustsFontSizeToFitWidth = true;
        label.Lines = lines;
        label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;

        // UILabel makes the text smaller on more than one line only when the line break mode truncates
        label.LineBreakMode = lines == 1 ? UILineBreakMode.Clip : UILineBreakMode.TailTruncation;
        label.MinimumScaleFactor = (nfloat)(min / max);

        // UILabel starts at the maximum size and goes down to the minimum scale factor
        if (label.Font.PointSize != (nfloat)max)
            label.Font = label.Font.WithSize((nfloat)max);
    }
}
