
#if ANDROID
using Android.Content;
using Android.Content.PM;
#endif
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maui.DynamicIcon;

public class DynamicIconManager
{
    private readonly Context _context;
    private readonly string _mainActivityClassName;
    private readonly List<string> _knownAliases;

    public DynamicIconManager(Context context, Type mainActivityType, IEnumerable<string> knownAliases)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mainActivityClassName = Java.Lang.Class.FromType(mainActivityType).Name;
        _knownAliases = knownAliases.ToList();
    }

    private IDynamicIconStrategy? _strategy;

    public void SetStrategy(IDynamicIconStrategy strategy) => _strategy = strategy;

    public void ApplyStrategy()
    {
        var alias = _strategy?.GetNextIconAlias();
        if (!string.IsNullOrEmpty(alias))
            SetIcon(alias);
    }

    public void SetAliasOnce(string alias)
    {
        SetStrategy(new StaticAliasStrategy(alias));
        ApplyStrategy();
    }

    public void ResetToDefault()
    {
        var pm = _context.PackageManager!;
        var mainComponent = new ComponentName(_context, _mainActivityClassName);

        // Desativa todos os aliases
        foreach (var alias in _knownAliases)
        {
            var aliasComponent = new ComponentName(_context, $"{_context.PackageName}.{alias}");
            pm.SetComponentEnabledSetting(aliasComponent, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);
        }

        // Ativa MainActivity
        pm.SetComponentEnabledSetting(mainComponent, ComponentEnabledState.Enabled, ComponentEnableOption.DontKillApp);
    }

    private void SetIcon(string aliasToActivate)
    {
        var pm = _context.PackageManager!;

        // Desativa MainActivity
        var main = new ComponentName(_context, _mainActivityClassName);
        pm.SetComponentEnabledSetting(main, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);

        foreach (var alias in _knownAliases)
        {
            var component = new ComponentName(_context, $"{_context.PackageName}.{alias}");
            var newState = alias == aliasToActivate ? ComponentEnabledState.Enabled : ComponentEnabledState.Disabled;
            pm.SetComponentEnabledSetting(component, newState, ComponentEnableOption.DontKillApp);
        }
    }
}
