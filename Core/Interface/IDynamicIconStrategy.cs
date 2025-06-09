#if ANDROID
#endif
namespace Maui.DynamicIcon;

public interface IDynamicIconStrategy
{
    string? GetNextIconAlias();
}
