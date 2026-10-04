namespace Plugin.Maui.FitText.Sample;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();

		UpdateLiveControls();
	}

	void OnLiveValuesChanged(object sender, EventArgs e) => UpdateLiveControls();

	void UpdateLiveControls()
	{
		// The handlers of the Entry and the Slider run during InitializeComponent, before the page has all its controls
		if (LiveLabel is null || LiveButton is null)
			return;

		LiveLabel.Text = LiveButton.Text = TextEntry.Text;
		LiveLabel.WidthRequest = LiveButton.WidthRequest = WidthSlider.Value;
	}
}
