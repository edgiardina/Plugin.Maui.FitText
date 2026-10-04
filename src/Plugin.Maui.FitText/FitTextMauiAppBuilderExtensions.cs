using Plugin.Maui.FitText.Controls;

namespace Plugin.Maui.FitText;

public static class FitTextMauiAppBuilderExtensions
{
    /// <summary>
    /// Registers the handlers of <see cref="FitTextLabel"/> and <see cref="FitTextButton"/>.
    /// </summary>
    public static MauiAppBuilder UseFitText(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler<FitTextLabel, FitTextLabelHandler>();
            handlers.AddHandler<FitTextButton, FitTextButtonHandler>();
        });

        return builder;
    }
}
