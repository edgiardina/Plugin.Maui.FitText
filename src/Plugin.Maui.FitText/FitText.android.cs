using Android.Widget;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Button;
using Microsoft.Maui.Handlers;

namespace Plugin.Maui.FitText;

public partial class FitTextLabelHandler : LabelHandler
{
    protected override void ConnectHandler(AppCompatTextView platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetAutoSizeTextTypeWithDefaults(AutoSizeTextType.Uniform);
    }
}

public partial class FitTextButtonHandler : ButtonHandler
{
    protected override void ConnectHandler(MaterialButton platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetAutoSizeTextTypeWithDefaults(AutoSizeTextType.Uniform);
    }
}