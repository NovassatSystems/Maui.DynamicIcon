using System;

namespace Maui.DynamicIcon;

public static class DynamicIcon
{
#if ANDROID
    public static void SetIcon<TMainActivity>(string aliasName, params string[] knownAliases) where TMainActivity : class
        => IconSwitcher.SetIcon(Android.App.Application.Context, typeof(TMainActivity), aliasName, knownAliases);
#endif
}
