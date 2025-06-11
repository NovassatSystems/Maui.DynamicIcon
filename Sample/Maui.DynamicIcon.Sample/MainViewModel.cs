using Maui.DynamicIcon;

namespace Maui.DynamicIcon.Sample;

public class MainViewModel(DynamicIconManager iconManager)
{
    readonly DynamicIconManager _iconManager = iconManager;
    int _counter = 0;
    public async Task ToggleIconWithDynamicIconCurrent()
    {
        var alias = _counter++ % 2 == 0 ? "Icon1" : "Icon2";
        await DynamicIcon.Current.SetIconAsync(alias);
    }

    public async void ToggleIconWithDynamicIconManager()
    {
        var alias = _counter++ % 2 == 0 ? "Icon1" : "Icon2";
        await _iconManager.SetAliasOnceAsync(alias);
    }

    public async void ToggleIconWithDynamicIconManagerAndStrategy()
    {
        _iconManager.SetStrategy(new SingleUseStrategy(true));
        await _iconManager.ApplyStrategyAsync();
    }
}
