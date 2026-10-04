using Microsoft.Maui.Handlers;
using Plugin.Maui.FitText.Controls;

namespace Plugin.Maui.FitText;

public partial class FitTextButtonHandler : ButtonHandler
{
    // .NET MAUI sets the native font or the native line settings again when one of these properties changes
    static readonly string[] RefitKeys =
    [
        nameof(ITextStyle.Font),
        nameof(IText.Text),
        nameof(ITextStyle.CharacterSpacing),
        nameof(IPadding.Padding),
        nameof(IButtonStroke.StrokeThickness),
        nameof(Button.TextTransform),
        nameof(Button.LineBreakMode),
    ];

    public static readonly IPropertyMapper<FitTextButton, FitTextButtonHandler> FitTextMapper = CreateMapper();

    public FitTextButtonHandler() : base(FitTextMapper)
    {
    }

    static PropertyMapper<FitTextButton, FitTextButtonHandler> CreateMapper()
    {
        var mapper = new PropertyMapper<FitTextButton, FitTextButtonHandler>(Mapper)
        {
            [nameof(FitTextButton.MinFontSize)] = MapFitText,
            [nameof(FitTextButton.MaxFontSize)] = MapFitText,
        };

        foreach (var key in RefitKeys)
        {
            mapper[key] = (handler, button) =>
            {
                Mapper.UpdateProperty(handler, button, key);
                MapFitText(handler, button);
            };
        }

        return mapper;
    }

    static void MapFitText(FitTextButtonHandler handler, FitTextButton button) => handler.UpdateFitText(button);

    partial void UpdateFitText(FitTextButton button);
}
