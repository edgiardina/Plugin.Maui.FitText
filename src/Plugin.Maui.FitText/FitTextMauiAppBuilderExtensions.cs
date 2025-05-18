using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Plugin.Maui.FitText.Controls;

namespace Plugin.Maui.FitText;

public static class FitTextMauiAppBuilderExtensions
{
    public static MauiAppBuilder UseFitText(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(handlers =>
        {
#if ANDROID
            handlers.AddHandler<FitTextLabel, FitTextLabelHandler>();
            handlers.AddHandler<FitTextButton, FitTextButtonHandler>();
#elif IOS
            handlers.AddHandler<FitTextLabel, FitTextLabelHandler>();
            handlers.AddHandler<FitTextButton, FitTextButtonHandler>();
#endif
        });

        return builder;
    }
}
