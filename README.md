# Plugin.Maui.FitText

[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.FitText.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.FitText/)

`Plugin.Maui.FitText` is a .NET MAUI plugin with two controls, `FitTextLabel` and `FitTextButton`. Each control changes its font size to make the text fit the control.

![image](https://github.com/user-attachments/assets/b1b92faa-7d81-46f4-a883-c7a0ca5fc8e1)

![image](https://github.com/user-attachments/assets/526778a8-f6fb-404a-a848-e9019f1f79de)

## Features

- `FitTextLabel` is a `Label` and `FitTextButton` is a `Button`. All the properties of the base controls are available.
- `MinFontSize` and `MaxFontSize` set the range of the font size.
- The font size changes when the text, the size of the control, or the range changes.
- A `FitTextLabel` can use more than one line with `MaxLines`.

## Supported Platforms

| Platform     | Minimum version      | How the text is fitted                                   |
| ------------ | -------------------- | -------------------------------------------------------- |
| iOS          | 14.2                 | `UILabel.AdjustsFontSizeToFitWidth`                      |
| Mac Catalyst | 15.0                 | `UILabel.AdjustsFontSizeToFitWidth`                      |
| Android      | 5.0 (API 21)         | The autosize function of `TextView` (`TextViewCompat`)   |
| Windows      | 10.0.17763.0         | The plugin measures the text and sets the font size      |

## Supported .NET Versions

The package has assemblies for .NET 9 and .NET 10.

| App target | Minimum .NET MAUI version |
| ---------- | ------------------------- |
| .NET 9     | 9.0.0                     |
| .NET 10    | 10.0.0                    |

## Getting Started

### 1. Install the NuGet package

```bash
dotnet add package Plugin.Maui.FitText
```

The package is available on [NuGet](https://www.nuget.org/packages/Plugin.Maui.FitText).

### 2. Register the plugin in `MauiProgram.cs`

```csharp
using Plugin.Maui.FitText;

public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .UseFitText(); // <-- Registers the FitText handlers

    return builder.Build();
}
```

### 3. Use the controls

Add the XML namespace to the page:

```xml
xmlns:fittext="clr-namespace:Plugin.Maui.FitText.Controls;assembly=Plugin.Maui.FitText"
```

Then add the controls:

```xml
<fittext:FitTextLabel Text="Hello World"
                      MinFontSize="8"
                      MaxFontSize="80"
                      HorizontalOptions="Center"
                      VerticalOptions="Center"
                      BackgroundColor="LightGray"
                      WidthRequest="200"
                      HeightRequest="50" />

<fittext:FitTextButton Text="Tap Me"
                       MinFontSize="10"
                       MaxFontSize="60"
                       BackgroundColor="SlateBlue"
                       TextColor="White"
                       WidthRequest="200"
                       HeightRequest="50" />
```

## Properties

| Property      | Type   | Default | Description                                      |
| ------------- | ------ | ------- | ------------------------------------------------ |
| `MinFontSize` | double | `10`    | The smallest font size that the control can use. |
| `MaxFontSize` | double | `100`   | The largest font size that the control can use.  |

The two properties are bindable. A `MinFontSize` less than 1 becomes 1. If `MaxFontSize` is less than `MinFontSize`, the control uses `MinFontSize`.

## Behavior

- **Font size.** The controls ignore `FontSize`. Use `MinFontSize` and `MaxFontSize`. `FontFamily` and `FontAttributes` operate as usual.
- **Lines.** The text shows on one line. To let a `FitTextLabel` use more lines, set `MaxLines` to a value larger than 1. A `FitTextButton` always uses one line.
- **Size of the control.** Give the control its size from the layout: a `WidthRequest` and a `HeightRequest`, or a layout that fills the available space. The text fits the width and the height of the control. If the control has no height from the layout, its height comes from the text, and the result is different on each platform.
- **Text that does not fit.** If the text does not fit at `MinFontSize`, the control uses `MinFontSize` and the text is cut off.

### Platform notes

- **Android.** The font size is an integer in `sp`. The plugin rounds `MinFontSize` and `MaxFontSize`.
- **iOS and Mac Catalyst.** The label measures at `MaxFontSize`. A control with no size from the layout asks for the room that the text needs at `MaxFontSize`.
- **Windows.** The fit does not include the image of a `FitTextButton` that has an `ImageSource`.

## Sample

The `samples` directory has a .NET MAUI app that shows `FitTextLabel` and `FitTextButton` in different layouts. The last section of the page changes the text and the width while the app runs.

## License

MIT © 2025 Ed Giardina
