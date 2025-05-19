using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Maui.FitText.Controls
{
    public class FitTextLabel : Label
    {
        public static readonly BindableProperty MinFontSizeProperty =
            BindableProperty.Create(nameof(MinFontSize), typeof(double), typeof(FitTextLabel), 10.0);

        public static readonly BindableProperty MaxFontSizeProperty =
            BindableProperty.Create(nameof(MaxFontSize), typeof(double), typeof(FitTextLabel), 100.0);

        public double MinFontSize
        {
            get => (double)GetValue(MinFontSizeProperty);
            set => SetValue(MinFontSizeProperty, value);
        }

        public double MaxFontSize
        {
            get => (double)GetValue(MaxFontSizeProperty);
            set => SetValue(MaxFontSizeProperty, value);
        }
    }
}
