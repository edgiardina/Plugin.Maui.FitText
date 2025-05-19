using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Maui.FitText.Controls
{
    public class FitTextButton : Button
    {
        public FitTextButton()
        {
            SizeChanged += (s, e) => InvalidateMeasure();
        }

    }
}
