using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SemanticKernelDemo.ViewModels;

namespace SemanticKernelDemo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("CamingoCode-Regular.tff", "CamingoCodeRegular");
                fonts.AddFont("CamingoCode-Italic.tff", "CamingoCodeItalic");
                fonts.AddFont("CamingoCode-BoldItalic.tff", "CamingoCodeBoldItalic");
                fonts.AddFont("CamingoCode-Bold.tff", "CamingoCodeBold");
            });

        builder.Services.AddSingleton<MainPage, MainPageViewModel>();
        builder.Services.AddSemanticKernelSettings();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}