using CommunityToolkit.Maui.Media;

namespace UserAssistant;

public partial class App : Application
{
	public static IServiceProvider ServiceProvider { get; private set; }

	public App()
	{
		InitializeComponent();

		var mauiApp = MauiProgram.CreateMauiApp();
		ServiceProvider = mauiApp.Services;

		var speechToText = ServiceProvider.GetService<ISpeechToText>();
		var cancellationToken = new CancellationToken();
		MainPage = new MainPage(speechToText, cancellationToken);
	}

}
