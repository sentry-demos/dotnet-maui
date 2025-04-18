using DotNetMaui.Services;
using Microsoft.Extensions.Logging;

namespace DotNetMaui;

public static class MauiProgram
{
	public static bool DidCrashOnLastRun { get; private set; }
	
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			
			// This adds Sentry to your Maui application
            // .UseSentry(options =>
            // {
            //     // The DSN is the only required option.
            //     options.Dsn = Configuration.SentryDsn;
            //     options.Debug = true;
            //     
            //     // Block your user order data from going to Sentry
            //     options.SendDefaultPii = false;
            //     
            //     // Attach screenshots on errors
            //     options.AttachScreenshot = true;
            // })
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<IDataService, DataService>();
		builder.Services.AddTransient<ListViewModel>();
		builder.Services.AddTransient<CartViewModel>();
		builder.Services.AddTransient<OrderViewModel>();

		return builder.Build();
	}
}
