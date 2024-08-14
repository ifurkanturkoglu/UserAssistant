using System.Globalization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;

namespace UserAssistant;

public partial class MainPage : ContentPage
{
	readonly ISpeechToText speechToText;
	CancellationToken cancellationToken;

	public MainPage()
	{
		InitializeComponent();
		
	}
	public MainPage(ISpeechToText _speechToText,CancellationToken cancellationToken) : this()
	{
		speechToText = _speechToText;
		this.cancellationToken = cancellationToken;
	}

	async void ListenClicked(object sender, EventArgs e){
		Listen(cancellationToken);
	}
	async void Listen(CancellationToken cancellationToken)
	{
		var isGranted = await speechToText.RequestPermissions(cancellationToken);
		if (!isGranted)
		{
			await Toast.Make("Permission not granted").Show(CancellationToken.None);
			return;
		}

		var recognitionResult = await speechToText.ListenAsync(
											CultureInfo.CurrentCulture,
											new Progress<string>(partialText =>
											{
												SpeechTextLabel.Text += partialText + " ";
											}), cancellationToken);


		var a = recognitionResult.Exception;

		if (recognitionResult.IsSuccessful)
		{
			SpeechTextLabel.Text = recognitionResult.Text;
		}
		else
		{
			await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
		}
	}
}

