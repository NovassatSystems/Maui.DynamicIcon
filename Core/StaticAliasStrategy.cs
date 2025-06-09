#if ANDROID
#endif
namespace Maui.DynamicIcon;

internal class StaticAliasStrategy(string alias) : IDynamicIconStrategy
{
    readonly string _alias = alias;
    public string? GetNextIconAlias() => _alias;
}

