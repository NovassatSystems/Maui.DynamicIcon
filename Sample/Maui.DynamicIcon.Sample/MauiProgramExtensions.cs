using Maui.DynamicIcon;

namespace Maui.DynamicIcon.Sample;

public static class MauiProgramExtensions
{

    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder app)
    {

        app.Services.AddSingleton<DynamicIconManager>();
        return app;
    }

    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder app)
    {
        app.Services.AddTransient<MainPage>();
        return app;
    }

    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder app)
    {
        app.Services.AddTransient<MainViewModel>();
        return app;
    }

    
    
   

    
}
