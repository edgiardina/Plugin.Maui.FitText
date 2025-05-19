using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Maui.FitText.Controls
{
    public class FitTextLabel : Label
    {
        public FitTextLabel()
        {
            SizeChanged += (s, e) => InvalidateMeasure();
        }

        static FitTextLabel()
        {
            Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping("AdjustsFontSizeToFitWidth", (handler, view) =>
            {
#if IOS    
                handler.PlatformView.AdjustsFontSizeToFitWidth = true;
                handler.PlatformView.Lines = 1;
                handler.PlatformView.BaselineAdjustment = UIKit.UIBaselineAdjustment.AlignCenters;
                handler.PlatformView.LineBreakMode = UIKit.UILineBreakMode.Clip;
#endif    
            });
        }
    }
}
