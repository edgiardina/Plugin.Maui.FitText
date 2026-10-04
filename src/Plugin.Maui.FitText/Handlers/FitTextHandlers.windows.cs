using Microsoft.Maui.Graphics;
using Microsoft.Maui.Primitives;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Plugin.Maui.FitText.Controls;
using WSize = Windows.Foundation.Size;

namespace Plugin.Maui.FitText;

// WinUI does not have an autosize function for text. The handlers measure the text
// in a TextBlock that is not in the visual tree and set the largest font size that fits.

public partial class FitTextLabelHandler
{
    TextBlock? _probe;
    Size _arrangedSize;

    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        var size = base.GetDesiredSize(widthConstraint, heightConstraint);

        if (VirtualView is not FitTextLabel label)
            return size;

        var textBlock = PlatformView;
        var probe = PrepareProbe(textBlock);
        var lines = FitTextRange.Lines(label);
        var (_, max) = FitTextRange.Normalize(label.MinFontSize, label.MaxFontSize);
        var availableWidth = widthConstraint - textBlock.Padding.Left - textBlock.Padding.Right;

        return TextFit.GrowToFontSize(size, probe, textBlock.FontSize, max, availableWidth, lines, VirtualView, widthConstraint, heightConstraint);
    }

    public override void PlatformArrange(Rect rect)
    {
        base.PlatformArrange(rect);

        _arrangedSize = rect.Size;

        if (VirtualView is FitTextLabel label)
            UpdateFitText(label);
    }

    partial void UpdateFitText(FitTextLabel label)
    {
        var textBlock = PlatformView;
        var lines = FitTextRange.Lines(label);

        var wrapping = lines == 1 ? TextWrapping.NoWrap : TextWrapping.Wrap;

        if (textBlock.TextWrapping != wrapping)
            textBlock.TextWrapping = wrapping;

        if (textBlock.MaxLines != lines)
            textBlock.MaxLines = lines;

        var padding = textBlock.Padding;
        var width = _arrangedSize.Width - padding.Left - padding.Right;
        var height = _arrangedSize.Height - padding.Top - padding.Bottom;

        // The control does not have a size before the first arrange
        if (width <= 0 || height <= 0)
            return;

        var (min, max) = FitTextRange.Normalize(label.MinFontSize, label.MaxFontSize);
        var fontSize = TextFit.Find(PrepareProbe(textBlock), min, max, width, height, lines);

        if (Math.Abs(textBlock.FontSize - fontSize) > TextFit.FontSizeTolerance)
            textBlock.FontSize = fontSize;
    }

    TextBlock PrepareProbe(TextBlock textBlock)
    {
        var probe = _probe ??= new TextBlock();

        probe.Text = textBlock.Text;
        probe.FontFamily = textBlock.FontFamily;
        probe.FontWeight = textBlock.FontWeight;
        probe.FontStyle = textBlock.FontStyle;
        probe.FontStretch = textBlock.FontStretch;
        probe.CharacterSpacing = textBlock.CharacterSpacing;
        probe.LineHeight = textBlock.LineHeight;
        probe.LineStackingStrategy = textBlock.LineStackingStrategy;

        return probe;
    }
}

public partial class FitTextButtonHandler
{
    TextBlock? _probe;
    Size _arrangedSize;

    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        var size = base.GetDesiredSize(widthConstraint, heightConstraint);

        if (VirtualView is not FitTextButton button || PrepareProbe() is not TextBlock probe)
            return size;

        var (_, max) = FitTextRange.Normalize(button.MinFontSize, button.MaxFontSize);

        return TextFit.GrowToFontSize(size, probe, PlatformView.FontSize, max, double.PositiveInfinity, 1, VirtualView, widthConstraint, heightConstraint);
    }

    public override void PlatformArrange(Rect rect)
    {
        base.PlatformArrange(rect);

        _arrangedSize = rect.Size;

        if (VirtualView is FitTextButton button)
            UpdateFitText(button);
    }

    partial void UpdateFitText(FitTextButton button)
    {
        var platformButton = PlatformView;
        var padding = platformButton.Padding;
        var border = platformButton.BorderThickness;
        var width = _arrangedSize.Width - padding.Left - padding.Right - border.Left - border.Right;
        var height = _arrangedSize.Height - padding.Top - padding.Bottom - border.Top - border.Bottom;

        // The control does not have a size before the first arrange
        if (width <= 0 || height <= 0 || PrepareProbe() is not TextBlock probe)
            return;

        var (min, max) = FitTextRange.Normalize(button.MinFontSize, button.MaxFontSize);
        var fontSize = TextFit.Find(probe, min, max, width, height, 1);

        if (Math.Abs(platformButton.FontSize - fontSize) > TextFit.FontSizeTolerance)
            platformButton.FontSize = fontSize;
    }

    // The TextBlock of the button gets its font from the button
    TextBlock? PrepareProbe()
    {
        var platformButton = PlatformView;

        if (platformButton.Content is not Panel content || content.Children.OfType<TextBlock>().FirstOrDefault() is not TextBlock textBlock)
            return null;

        var probe = _probe ??= new TextBlock();

        probe.Text = textBlock.Text;
        probe.FontFamily = platformButton.FontFamily;
        probe.FontWeight = platformButton.FontWeight;
        probe.FontStyle = platformButton.FontStyle;
        probe.FontStretch = platformButton.FontStretch;
        probe.CharacterSpacing = platformButton.CharacterSpacing;

        return probe;
    }
}

static class TextFit
{
    public const double FontSizeTolerance = 0.01;

    // WinUI rounds the measured size to full pixels
    const double SizeTolerance = 0.5;
    const double FontSizeStep = 0.25;

    /// <summary>
    /// Finds the largest font size in the range for which the text of the probe fits the available size.
    /// </summary>
    public static double Find(TextBlock probe, double min, double max, double availableWidth, double availableHeight, int lines)
    {
        if (Fits(probe, max, availableWidth, availableHeight, lines))
            return max;

        if (!Fits(probe, min, availableWidth, availableHeight, lines))
            return min;

        var low = min;
        var high = max;

        while (high - low > FontSizeStep)
        {
            var middle = (low + high) / 2;

            if (Fits(probe, middle, availableWidth, availableHeight, lines))
                low = middle;
            else
                high = middle;
        }

        return low;
    }

    /// <summary>
    /// Adds the room that the text needs at the given font size to a size that was measured at the current font size.
    /// The result makes the layout give the control the same room as on iOS, where the label measures at the maximum font size.
    /// </summary>
    public static Size GrowToFontSize(Size size, TextBlock probe, double currentFontSize, double fontSize, double availableWidth, int lines, IView view, double widthConstraint, double heightConstraint)
    {
        var current = Measure(probe, currentFontSize, availableWidth, lines);
        var target = Measure(probe, fontSize, availableWidth, lines);

        var width = size.Width;
        var height = size.Height;

        if (!Dimension.IsExplicitSet(view.Width))
            width = Math.Max(width, Math.Min(width + target.Width - current.Width, Math.Min(widthConstraint, view.MaximumWidth)));

        if (!Dimension.IsExplicitSet(view.Height))
            height = Math.Max(height, Math.Min(height + target.Height - current.Height, Math.Min(heightConstraint, view.MaximumHeight)));

        return new Size(width, height);
    }

    static bool Fits(TextBlock probe, double fontSize, double availableWidth, double availableHeight, int lines)
    {
        var size = Measure(probe, fontSize, availableWidth, lines);

        if (size.Width > availableWidth + SizeTolerance || size.Height > availableHeight + SizeTolerance)
            return false;

        if (lines == 1)
            return true;

        // The wrapped text must not use more lines than the limit
        var lineHeight = Measure(probe, fontSize, double.PositiveInfinity, 1).Height;

        return size.Height <= lines * lineHeight + SizeTolerance;
    }

    static WSize Measure(TextBlock probe, double fontSize, double availableWidth, int lines)
    {
        probe.FontSize = fontSize;
        probe.TextWrapping = lines == 1 ? TextWrapping.NoWrap : TextWrapping.Wrap;
        probe.Measure(new WSize(lines == 1 ? double.PositiveInfinity : Math.Max(availableWidth, 0), double.PositiveInfinity));

        return probe.DesiredSize;
    }
}
