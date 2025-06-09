#if ANDROID
#endif
namespace Maui.DynamicIcon;

internal class StaticAliasStrategy(string alias) : IDynamicIconStrategy
{
    private readonly string _alias = alias;
    public string? GetNextIconAlias() => _alias;
}

