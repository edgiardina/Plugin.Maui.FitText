namespace Plugin.Maui.FitText.Controls
{
    /// <summary>
    /// A <see cref="Button"/> that changes its font size to make the text fit the control.
    /// </summary>
    /// <remarks>
    /// The control ignores <see cref="Button.FontSize"/>. The text shows on one line.
    /// </remarks>
    public class FitTextButton : Button
    {
        public static readonly BindableProperty MinFontSizeProperty =
            BindableProperty.Create(nameof(MinFontSize), typeof(double), typeof(FitTextButton), FitTextRange.DefaultMinFontSize);

        public static readonly BindableProperty MaxFontSizeProperty =
            BindableProperty.Create(nameof(MaxFontSize), typeof(double), typeof(FitTextButton), FitTextRange.DefaultMaxFontSize);

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
