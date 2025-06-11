#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.PM;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maui.DynamicIcon.Platforms.Android;

public class DynamicIconServiceAndroid : IDynamicIconService
{
    private readonly Context _context;
    private readonly string _mainActivity;
    private readonly List<string> _aliases;

    public DynamicIconServiceAndroid(IEnumerable<string> aliases)
    {
        _context = Application.Context;
        _mainActivity = DiscoverMainActivityName()
            ?? throw new InvalidOperationException("MainActivity não encontrada.");
        _aliases = [.. aliases];
    }

    public bool CanChangeIcon => true;

    public string? GetCurrentIconName() => null;

    public async Task<bool> SetIconAsync(string? alias)
    {
        try
        {
            var pm = _context.PackageManager!;
            var main = new ComponentName(_context, _mainActivity);
            pm.SetComponentEnabledSetting(main, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);

            foreach (var name in _aliases)
            {
                var comp = new ComponentName(_context, $"{_context.PackageName}.{name}");
                var state = name == alias ? ComponentEnabledState.Enabled : ComponentEnabledState.Disabled;
                pm.SetComponentEnabledSetting(comp, state, ComponentEnableOption.DontKillApp);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    private string? DiscoverMainActivityName()
    {
        var intent = _context.PackageManager?.GetLaunchIntentForPackage(_context.PackageName!);
        var info = _context.PackageManager?.ResolveActivity(intent!, PackageInfoFlags.MatchDefaultOnly);
        return info?.ActivityInfo?.Name;
    }
}
#endif
