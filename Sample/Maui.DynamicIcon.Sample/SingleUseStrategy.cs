namespace Maui.DynamicIcon.Sample;

public class SingleUseStrategy(bool mustBeChange) : IDynamicIconStrategy
{

    private readonly string _alias = mustBeChange ? "Icon1" : "Icon2";
    public string? GetNextIconAlias() => _alias;
}
