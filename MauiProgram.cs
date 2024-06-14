using Microsoft.Extensions.Logging;

namespace Biathlon_Esthivaukan
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
                    fonts.AddFont("Lexend-VariableFont_wght.ttf", "Lexend-Regular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


       
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
