using Microsoft.Maui.Graphics;
using Plugin.Maui.FitText.Controls;
using UIKit;

namespace Plugin.Maui.FitText;

public partial class FitTextLabelHandler
{
    double _availableHeight = double.PositiveInfinity;

    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        if (VirtualView is not FitTextLabel label)
            return base.GetDesiredSize(widthConstraint, heightConstraint);

        // Measure at the maximum font size, not at the size that the last arrange gave
        AdjustsFontSize.Apply(PlatformView, label.MinFontSize, label.MaxFontSize, FitTextRange.Lines(label), double.PositiveInfinity);
        var size = base.GetDesiredSize(widthConstraint, heightConstraint);
        UpdateFitText(label);

        return size;
    }

    public override void PlatformArrange(Rect rect)
    {
        base.PlatformArrange(rect);

        if (VirtualView is not FitTextLabel label)
            return;

        _availableHeight = rect.Height - label.Padding.VerticalThickness;
        UpdateFitText(label);
    }

    partial void UpdateFitText(FitTextLabel label) =>
        AdjustsFontSize.Apply(PlatformView, label.MinFontSize, label.MaxFontSize, FitTextRange.Lines(label), _availableHeight);
}

public partial class FitTextButtonHandler
{
    double _availableHeight = double.PositiveInfinity;

    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        if (VirtualView is not FitTextButton button || PlatformView.TitleLabel is not UILabel titleLabel)
            return base.GetDesiredSize(widthConstraint, heightConstraint);

        // Measure at the maximum font size, not at the size that the last arrange gave
        AdjustsFontSize.Apply(titleLabel, button.MinFontSize, button.MaxFontSize, 1, double.PositiveInfinity);
        var size = base.GetDesiredSize(widthConstraint, heightConstraint);
        UpdateFitText(button);

        return size;
    }

    public override void PlatformArrange(Rect rect)
    {
        base.PlatformArrange(rect);

        if (VirtualView is not FitTextButton button)
            return;

        var padding = button.Padding.IsNaN ? 0 : button.Padding.VerticalThickness;

        _availableHeight = rect.Height - padding - 2 * button.BorderWidth;
        UpdateFitText(button);
    }

    partial void UpdateFitText(FitTextButton button)
    {
        if (PlatformView.TitleLabel is UILabel titleLabel)
            AdjustsFontSize.Apply(titleLabel, button.MinFontSize, button.MaxFontSize, 1, _availableHeight);
    }
}

static class AdjustsFontSize
{
    const double FontSizeStep = 0.25;

    public static void Apply(UILabel label, double minFontSize, double maxFontSize, int lines, double availableHeight)
    {
        var (min, max) = FitTextRange.Normalize(minFontSize, maxFontSize);

        // UILabel fits the text to the width only. The height of the control sets the largest size that UILabel can start from.
        var fontSize = max;
        var lineHeight = (double)label.Font.WithSize((nfloat)max).LineHeight;

        if (availableHeight > 0 && !double.IsInfinity(availableHeight) && lineHeight > 0)
        {
            var limit = max * availableHeight / (lineHeight * lines);

            fontSize = Math.Clamp(Math.Floor(limit / FontSizeStep) * FontSizeStep, min, max);
        }

        label.AdjustsFontSizeToFitWidth = true;
        label.Lines = lines;
        label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;

        // UILabel makes the text smaller on more than one line only when the line break mode truncates
        label.LineBreakMode = lines == 1 ? UILineBreakMode.Clip : UILineBreakMode.TailTruncation;
        label.MinimumScaleFactor = (nfloat)(min / fontSize);

        // UILabel starts at this size and goes down to the minimum scale factor
        if (label.Font.PointSize != (nfloat)fontSize)
            label.Font = label.Font.WithSize((nfloat)fontSize);
    }
}
