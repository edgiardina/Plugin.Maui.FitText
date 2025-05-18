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
<fittext:FitTextLabel
    Text="Auto Resizing Label"
    HorizontalOptions="Center"
    VerticalOptions="Center"
    WidthRequest="200"
    HeightRequest="50" />

<fittext:FitTextButton
    Text="Tap Me"
    WidthRequest="150"
    HeightRequest="60"
    Clicked="OnButtonClicked" />
```

In your XAML, don't forget the namespace:

```xml
xmlns:fittext="clr-namespace:Plugin.Maui.FitText;assembly=Plugin.Maui.FitText"
```

---

## Samples

A sample .NET MAUI app is included in the `/samples` directory, demonstrating how to use `FitTextLabel` and `FitTextButton`.

---

## Publish to NuGet

To publish:

1. Tag your release like `v1.0.0` or `v1.0.0-preview1`.
2. Push to GitHub.
3. Ensure the `NUGET_API_KEY` GitHub secret is configured with a valid NuGet.org API key.
4. The GitHub Action will build and publish the package.

More info: [Publish a NuGet Package](https://learn.microsoft.com/nuget/nuget-org/publish-a-package)

---

## License

MIT © 2025 Ed Giardina
