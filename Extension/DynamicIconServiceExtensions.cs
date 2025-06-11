#if ANDROID
using Maui.DynamicIcon.Platforms.Android;
#elif IOS
using Maui.DynamicIcon.Platforms.iOS;
#endif

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using System.Diagnostics;

namespace Maui.DynamicIcon;

public static class DynamicIconServiceExtensions
{
    public static MauiAppBuilder UseDynamicIcon(this MauiAppBuilder builder, params string[] aliases)
    {
#if ANDROID
    builder.Services.AddSingleton<IDynamicIconService>(provider =>
    {
        var service = new DynamicIconServiceAndroid(aliases);
        DynamicIcon.Initialize(service);
        Debug.WriteLine("Service inicializada");
        return service;
    });
#elif IOS
        builder.Services.AddSingleton<IDynamicIconService>(provider =>
        {
            var service = new DynamicIconServiceiOS();
            DynamicIcon.Initialize(service);
            Debug.WriteLine("Service inicializada");
            return service;
        });
#endif

        builder.Services.AddSingleton<DynamicIconManager>();
        return builder;
    }
}