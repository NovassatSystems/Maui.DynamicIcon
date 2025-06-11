namespace Maui.DynamicIcon.Sample;

public static class IconServiceExtensions
{
    public static void StartIconChangerIfAndroid(this MauiApp app)
    {
#if ANDROID
        var context = Android.App.Application.Context;
        var intent = new Android.Content.Intent(context, typeof(Platforms.Android.Services.IconChangerService));

        if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            context.StartForegroundService(intent);
        else
            context.StartService(intent);
#endif
    }
}
