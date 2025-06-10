
//#if ANDROID
//using Android.Content;
//using Android.Content.PM;
//using System;
//#endif

//namespace Maui.DynamicIcon;
//public static class IconSwitcher
//{
//    public static void SetIcon(Context context, Type mainActivityType, string aliasToActivate, string[] knownAliases)
//    {
//#if ANDROID
//        var pm = context.PackageManager!;
//        var mainComponent = new ComponentName(context, Java.Lang.Class.FromType(mainActivityType).Name);

//        pm.SetComponentEnabledSetting(mainComponent, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);

//        foreach (var alias in knownAliases)
//        {
//            var aliasComponent = new ComponentName(context, context.PackageName + "." + alias);
//            var newState = alias == aliasToActivate ? ComponentEnabledState.Enabled : ComponentEnabledState.Disabled;
//            pm.SetComponentEnabledSetting(aliasComponent, newState, ComponentEnableOption.DontKillApp);
//        }
//#endif
//    }
//}