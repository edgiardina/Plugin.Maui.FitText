using Plugin.Maui.FitText.Controls;
using Microsoft.Maui.Handlers;

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
#elif IOS || MACCATALYST
        LabelHandler.Mapper.AppendToMapping("AdjustsFontSizeToFitWidth", (LabelHandler handler, Label view) =>
        {
            if (view is not FitTextLabel label || handler.PlatformView is not UIKit.UILabel nativeLabel)
                return;

            nativeLabel.AdjustsFontSizeToFitWidth = true;
            nativeLabel.Lines = 1;
            nativeLabel.BaselineAdjustment = UIKit.UIBaselineAdjustment.AlignCenters;
            nativeLabel.LineBreakMode = UIKit.UILineBreakMode.Clip;

            var max = label.MaxFontSize;
            var min = label.MinFontSize;

            nativeLabel.MinimumScaleFactor = (nfloat)(min / max);
            nativeLabel.Font = nativeLabel.Font.WithSize((nfloat)max);
        });

        ButtonHandler.Mapper.AppendToMapping("AdjustsFontSizeToFitWidth", (ButtonHandler handler, Button view) =>
        {
            if (view is not FitTextButton button || handler.PlatformView?.TitleLabel is not UIKit.UILabel titleLabel)
                return;

            titleLabel.AdjustsFontSizeToFitWidth = true;
            titleLabel.Lines = 1;
            titleLabel.BaselineAdjustment = UIKit.UIBaselineAdjustment.AlignCenters;
            titleLabel.LineBreakMode = UIKit.UILineBreakMode.Clip;

            var max = button.MaxFontSize;
            var min = button.MinFontSize;

            titleLabel.MinimumScaleFactor = (nfloat)(min / max);
            titleLabel.Font = titleLabel.Font.WithSize((nfloat)max);
        });
#endif
        });

        return builder;
    }
}
