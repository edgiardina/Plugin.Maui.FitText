# Plugin.Maui.FitText

`Plugin.Maui.FitText` is a .NET MAUI plugin that provides two drop-in controls — `FitTextLabel` and `FitTextButton` — whose text automatically resizes to fit the available space. It uses platform-native font scaling on iOS and Android for optimal performance and appearance.

---

## Features

- 📏 Auto-resizing `Label` and `Button` controls.
- 🧠 Shared logic for dynamic font size calculation.
- ⚙️ Native support for Android (`AutoSizeTextType.Uniform`) and iOS (`AdjustsFontSizeToFitWidth`).
- ✅ Simple integration: just call `.UseFitText()` during startup.

---

## Getting Started

### 1. Install the NuGet Package

```bash
dotnet add package Plugin.Maui.FitText
```

[![NuGet](https://img.shields.io/nuget/v/Plugin.Maui.FitText.svg?label=NuGet)](https://www.nuget.org/packages/Plugin.Maui.FitText/)

Available on [NuGet](http://www.nuget.org/packages/Plugin.Maui.FitText).

### 2. Register the Plugin in `MauiProgram.cs`

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder()
        .UseMauiApp<App>()
        .UseFitText(); // <-- Register the FitText handlers here

    return builder.Build();
}
```

### 3. Use the Controls

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

In your XAML, don't forget the namespace:

```xml
xmlns:fittext="clr-namespace:Plugin.Maui.FitText;assembly=Plugin.Maui.FitText"
```

---

### Optional Properties

| Property       | Type   | Description                                                  |
|----------------|--------|--------------------------------------------------------------|
| `MinFontSize`  | double | The smallest font size allowed when shrinking. Default: `10` |
| `MaxFontSize`  | double | The starting size and upper bound. Default: `100`            |


## Samples

A sample .NET MAUI app is included in the `/samples` directory, demonstrating how to use `FitTextLabel` and `FitTextButton`.

---

## License

MIT © 2025 Ed Giardina
