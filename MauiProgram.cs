using DotNetMaui.Services;
using Microsoft.Extensions.Logging;
using Sentry.Maui;

namespace DotNetMaui;

public static class MauiProgram
{
	
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			
			// This adds Sentry to your Maui application
            .UseSentry(options =>
            {
                // The DSN is the only required option.
                options.Dsn = Configuration.SentryDsn;
                options.Debug = true;
                
                // Set to false if you want to prevent your user order data from going to Sentry
                options.SendDefaultPii = true;
                
                // Attach screenshots on errors
                options.AttachScreenshot = true;
            })
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

		// a sample maui element binder that allows us to observe controls moving through the MAUI view hierarchy
		// this particular sample, will search out known command properties within a button or element gesture recognizers, to determine if the command
		// is an MVVM Community Toolkit async relay command to auto create a span for its execution time
		builder.Services.AddSingleton<IMauiElementEventBinder, CtMvvmMauiElementEventBinder>();

		return builder.Build();
	}
}
