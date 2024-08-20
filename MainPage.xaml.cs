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
	public MainPage(ISpeechToText _speechToText, CancellationToken cancellationToken) : this()
	{
		speechToText = _speechToText;
		this.cancellationToken = cancellationToken;
	}

	async void ListenClicked(object sender, EventArgs e)
	{
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
		

		PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
		

		if (status == PermissionStatus.Granted)
		{
			if (recognitionResult.IsSuccessful)
			{
				SpeechTextLabel.Text = recognitionResult.Text;
				List<string> words = recognitionResult.Text.Split(" ").ToList();
				int index = words.FindIndex(word => word.Contains("aç"));
				if (index > 0)
				{
					string appName = words[index - 1];
					int apostropheIndex = appName.IndexOf("'");
					if (apostropheIndex != -1)
					{
						appName = appName.Substring(0, apostropheIndex);
					}
					appName = appName.ToLower();

					try
					{
						#if WINDOWS
							appName = "epic games";
						
						#endif
						LaunchAppWithUri.LaunchApp(appName,"open");
					}
					catch (Exception e)
					{
						
						await Toast.Make(e.Message).Show();
					}
					
				}
			}
		}


		else
		{
			await Toast.Make(recognitionResult.Exception?.Message ?? "Unable to recognize speech").Show(CancellationToken.None);
		}
	}


	void OpenApp(object sender, EventArgs e)
	{
		LaunchAppWithUri.LaunchApp("spotify","open");
	}

}

