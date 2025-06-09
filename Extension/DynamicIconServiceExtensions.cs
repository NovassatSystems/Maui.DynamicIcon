
#if ANDROID
#endif
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace Maui.DynamicIcon;

public static class DynamicIconServiceExtensions
{
    public static MauiAppBuilder UseDynamicIcon<TMainActivity>(this MauiAppBuilder builder, params string[] knownAliases) where TMainActivity : class
    {
#if ANDROID
        builder.Services.AddSingleton(sp =>
        {
            var context = Android.App.Application.Context;
            return new DynamicIconManager(context, typeof(TMainActivity), knownAliases);
        });
#endif
        return builder;
    }
}