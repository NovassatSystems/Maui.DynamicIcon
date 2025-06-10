#if ANDROID
using Android.Content;
using Android.Content.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maui.DynamicIcon.Platforms.Android;

public class DynamicIconServiceAndroid : IDynamicIconService
{
    private readonly Context _context;
    private readonly string _mainActivityClassName;
    private readonly List<string> _aliases;

    public DynamicIconServiceAndroid(Context context, Type mainActivityType, IEnumerable<string> aliases)
    {
        _context = context;
        _mainActivityClassName = Java.Lang.Class.FromType(mainActivityType).Name;
        _aliases = aliases.ToList();
    }

    public bool CanChangeIcon => true;

    public string? GetCurrentIconName() => null; 

    public async Task<bool> SetIconAsync(string? iconName)
    {
        try
        {
            var pm = _context.PackageManager!;
            var main = new ComponentName(_context, _mainActivityClassName);

            pm.SetComponentEnabledSetting(main, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);

            foreach (var alias in _aliases)
            {
                var comp = new ComponentName(_context, $"{_context.PackageName}.{alias}");
                var state = alias == iconName ? ComponentEnabledState.Enabled : ComponentEnabledState.Disabled;
                pm.SetComponentEnabledSetting(comp, state, ComponentEnableOption.DontKillApp);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
#endif
