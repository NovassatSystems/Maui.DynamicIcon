using System.Threading.Tasks;

namespace Maui.DynamicIcon;

public class DynamicIconManager(IDynamicIconService service)
{
    readonly IDynamicIconService _service = service;
    IDynamicIconStrategy? _strategy;

    public void SetStrategy(IDynamicIconStrategy strategy) => _strategy = strategy;

    public async Task<bool> ApplyStrategyAsync()
    {
        var nextAlias = _strategy?.GetNextIconAlias();
        if (!string.IsNullOrWhiteSpace(nextAlias))
            return await _service.SetIconAsync(nextAlias);

        return false;
    }

    public Task<bool> SetAliasOnceAsync(string alias)
    {
        SetStrategy(new StaticAliasStrategy(alias));
        return ApplyStrategyAsync();
    }

    public Task<bool> ResetToDefaultAsync() => _service.SetIconAsync(null);
}
