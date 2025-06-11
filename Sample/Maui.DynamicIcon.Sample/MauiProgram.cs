using Microsoft.Extensions.Logging;
using Maui.DynamicIcon;

namespace Maui.DynamicIcon.Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDynamicIcon("Icon1", "Icon2", "Icon3")
                .ConfigurePages()
                .ConfigureViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
