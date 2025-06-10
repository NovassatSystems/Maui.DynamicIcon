#if ANDROID
using Maui.DynamicIcon.Platforms.Android;
#elif IOS
using Maui.DynamicIcon.Platforms.iOS;
#endif

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace Maui.DynamicIcon;

public static class DynamicIconServiceExtensions
{
    public static MauiAppBuilder UseDynamicIcon<T>(this MauiAppBuilder builder, params string[] aliases)
    {
#if ANDROID
    builder.Services.AddSingleton<IDynamicIconService>(provider =>
    {
        var service = new DynamicIconServiceAndroid(Android.App.Application.Context, typeof(T), aliases);
        DynamicIcon.Initialize(service);
        return service;
    });
#elif IOS
        builder.Services.AddSingleton<IDynamicIconService>(provider =>
        {
            var service = new DynamicIconServiceiOS();
            DynamicIcon.Initialize(service);
            return service;
        });
#endif
        return builder;
    }
}