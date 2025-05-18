using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace Plugin.Maui.FitText;

public partial class FitTextLabelHandler : LabelHandler
{
    protected override void ConnectHandler(MauiLabel platformView)
    {
        base.ConnectHandler(platformView);

        platformView.AdjustsFontSizeToFitWidth = true;
        platformView.MinimumScaleFactor = 0.5f;
    }
}

public partial class FitTextButtonHandler : ButtonHandler
{
    protected override void ConnectHandler(UIButton nativeView)
    {
        base.ConnectHandler(nativeView);
        nativeView.TitleLabel.AdjustsFontSizeToFitWidth = true;
        nativeView.TitleLabel.MinimumScaleFactor = 0.5f;
    }
}