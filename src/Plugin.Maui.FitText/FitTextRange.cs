namespace Plugin.Maui.FitText;

static class FitTextRange
{
    public const double DefaultMinFontSize = 10.0;
    public const double DefaultMaxFontSize = 100.0;

    const double SmallestFontSize = 1.0;
    const double LargestFontSize = 2000.0;

    /// <summary>
    /// Makes a range that the platforms can use: the minimum is 1 or more, and the maximum is not less than the minimum.
    /// </summary>
    public static (double Min, double Max) Normalize(double min, double max)
    {
        if (double.IsNaN(min))
            min = DefaultMinFontSize;
        if (double.IsNaN(max))
            max = DefaultMaxFontSize;

        min = Math.Clamp(min, SmallestFontSize, LargestFontSize);
        max = Math.Clamp(max, min, LargestFontSize);

        return (min, max);
    }

    /// <summary>
    /// The number of lines that the text of a label can use.
    /// </summary>
    public static int Lines(Label label) => label.MaxLines > 0 ? label.MaxLines : 1;
}
