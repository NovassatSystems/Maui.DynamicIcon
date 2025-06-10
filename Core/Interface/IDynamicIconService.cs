using System.Threading.Tasks;

namespace Maui.DynamicIcon;
public interface IDynamicIconService
{
    bool CanChangeIcon { get; }
    string? GetCurrentIconName();
    Task<bool> SetIconAsync(string? iconName); // null = reset to default
}