using Microsoft.Extensions.Logging;
using MonkeyFinder.View;
using MonkeyFinder.Services;  // Importing the monkey service class

namespace MonkeyFinder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        // Registering our main page with builder.Services
        builder.Services.AddSingleton<MainPage>();
		// Registering our MonkeyService and MonkeysViewModel with builder.Services
		builder.Services.AddSingleton<MonkeyService>();
		builder.Services.AddSingleton<MonkeysViewModel>();

		return builder.Build();
	}
}
