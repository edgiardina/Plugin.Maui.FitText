using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Button;
using Microsoft.Maui.Handlers;
using Plugin.Maui.FitText.Controls;

namespace Plugin.Maui.FitText;

public partial class FitTextLabelHandler : LabelHandler
{
    protected override void ConnectHandler(AppCompatTextView platformView)
    {
        base.ConnectHandler(platformView);
        if (VirtualView is FitTextLabel fit)
        {
            platformView.SetAutoSizeTextTypeUniformWithConfiguration(
                (int)fit.MinFontSize,
                (int)fit.MaxFontSize,
                1,
                (int)ComplexUnitType.Sp);
        }
        else
        {
            // fallback
            platformView.SetAutoSizeTextTypeWithDefaults(AutoSizeTextType.Uniform);
        }
    }
}

public partial class FitTextButtonHandler : ButtonHandler
{
    protected override void ConnectHandler(MaterialButton platformView)
    {
        base.ConnectHandler(platformView);
        if (VirtualView is FitTextButton fit)
        {
            platformView.SetAutoSizeTextTypeUniformWithConfiguration(
                (int)fit.MinFontSize,
                (int)fit.MaxFontSize,
                1,
                (int)ComplexUnitType.Sp);
        }
        else
        {
            // fallback
            platformView.SetAutoSizeTextTypeWithDefaults(AutoSizeTextType.Uniform);
        }
    }
}