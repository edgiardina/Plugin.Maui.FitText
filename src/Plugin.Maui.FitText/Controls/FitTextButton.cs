using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Maui.FitText.Controls
{
    public class FitTextButton : Button
    {
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            AdjustFontSize(width, height);
        }

        private void AdjustFontSize(double width, double height)
        {
            if (string.IsNullOrEmpty(Text)) return;
            FontSize = FitTextHelper.FindBestFontSize(Text, FontFamily, width, height);
        }
    }
}
