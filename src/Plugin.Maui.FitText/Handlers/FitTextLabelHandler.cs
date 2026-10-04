using Microsoft.Maui.Handlers;
using Plugin.Maui.FitText.Controls;

namespace Plugin.Maui.FitText;

public partial class FitTextLabelHandler : LabelHandler
{
    // .NET MAUI sets the native font or the native line settings again when one of these properties changes
    static readonly string[] RefitKeys =
    [
        nameof(ITextStyle.Font),
        nameof(IText.Text),
        nameof(ITextStyle.CharacterSpacing),
        nameof(ILabel.LineHeight),
        nameof(IPadding.Padding),
        nameof(Label.TextTransform),
        nameof(Label.TextType),
        nameof(Label.FormattedText),
        nameof(Label.LineBreakMode),
        nameof(Label.MaxLines),
    ];

    public static readonly IPropertyMapper<FitTextLabel, FitTextLabelHandler> FitTextMapper = CreateMapper();

    public FitTextLabelHandler() : base(FitTextMapper)
    {
    }

    static PropertyMapper<FitTextLabel, FitTextLabelHandler> CreateMapper()
    {
        var mapper = new PropertyMapper<FitTextLabel, FitTextLabelHandler>(Mapper)
        {
            [nameof(FitTextLabel.MinFontSize)] = MapFitText,
            [nameof(FitTextLabel.MaxFontSize)] = MapFitText,
        };

        foreach (var key in RefitKeys)
        {
            mapper[key] = (handler, label) =>
            {
                Mapper.UpdateProperty(handler, label, key);
                MapFitText(handler, label);
            };
        }

        return mapper;
    }

    static void MapFitText(FitTextLabelHandler handler, FitTextLabel label) => handler.UpdateFitText(label);

    partial void UpdateFitText(FitTextLabel label);
}
