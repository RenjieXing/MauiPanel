using BiliBiliMaui.Services;
using BiliBiliMaui.ViewModels;
using Microsoft.Extensions.Logging;

namespace BiliBiliMaui
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterViewModels()
                ;

            builder.Services.AddSingleton<IServiceProvider>(builder.Services.BuildServiceProvider());


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }


        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<VideosService>();
            mauiAppBuilder.Services.AddSingleton<VideosModelViews>();
#if ANDROID
            mauiAppBuilder.Services.AddSingleton(MediaPicker.Default);
#endif
            return mauiAppBuilder;
        }
    }
}
