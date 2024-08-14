using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Media;

namespace UserAssistant;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
		.UseMauiApp<App>()
		.UseMauiCommunityToolkit(
			options => {
				options.SetShouldEnableSnackbarOnWindows(true);
			}
			
		);

		builder.Services.AddSingleton<ISpeechToText,SpeechToTextImplementation>();
        return builder.Build();

	}
}
