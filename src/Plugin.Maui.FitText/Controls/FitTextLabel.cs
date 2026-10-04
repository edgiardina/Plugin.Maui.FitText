namespace Plugin.Maui.FitText.Controls
{
    /// <summary>
    /// A <see cref="Label"/> that changes its font size to make the text fit the control.
    /// </summary>
    /// <remarks>
    /// The control ignores <see cref="Label.FontSize"/>. The text shows on one line unless
    /// <see cref="Label.MaxLines"/> is larger than 1.
    /// </remarks>
    public class FitTextLabel : Label
    {
        public static readonly BindableProperty MinFontSizeProperty =
            BindableProperty.Create(nameof(MinFontSize), typeof(double), typeof(FitTextLabel), FitTextRange.DefaultMinFontSize);

        public static readonly BindableProperty MaxFontSizeProperty =
            BindableProperty.Create(nameof(MaxFontSize), typeof(double), typeof(FitTextLabel), FitTextRange.DefaultMaxFontSize);

        /// <summary>
        /// The smallest font size that the control can use. The default is 10.
        /// </summary>
        public double MinFontSize
        {
            get => (double)GetValue(MinFontSizeProperty);
            set => SetValue(MinFontSizeProperty, value);
        }

        /// <summary>
        /// The largest font size that the control can use. The default is 100.
        /// </summary>
        public double MaxFontSize
        {
            get => (double)GetValue(MaxFontSizeProperty);
            set => SetValue(MaxFontSizeProperty, value);
        }
    }
}
