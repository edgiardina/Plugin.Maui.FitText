namespace Plugin.Maui.FitText;

public static class FitTextHelper
{
    public static double FindBestFontSize(string text, string fontFamily, double width, double height, double minSize = 10, double maxSize = 100, double step = 1)
    {
        double bestSize = minSize;

        for (double size = minSize; size <= maxSize; size += step)
        {
            var sizeRequest = text.Measure(size, fontFamily, width, height);
            if (sizeRequest.Width > width || sizeRequest.Height > height)
                break;

            bestSize = size;
        }

        return bestSize;
    }

    private static Size Measure(this string text, double fontSize, string fontFamily, double widthConstraint, double heightConstraint)
    {
        var label = new Label
        {
            Text = text,
            FontSize = fontSize,
            FontFamily = fontFamily,
            LineBreakMode = LineBreakMode.WordWrap
        };
        label.Measure(widthConstraint, heightConstraint);
        return label.DesiredSize;
    }
}
