#if IOS
using System.Threading.Tasks;
using UIKit;

namespace Maui.DynamicIcon.Platforms.iOS;

public class DynamicIconServiceiOS : IDynamicIconService
{
    public bool CanChangeIcon => UIApplication.SharedApplication.SupportsAlternateIcons;

    public string? GetCurrentIconName() => UIApplication.SharedApplication.AlternateIconName;

    public async Task<bool> SetIconAsync(string? iconName)
    {
        if (!CanChangeIcon)
            return false;

        var tcs = new TaskCompletionSource<bool>();

        UIApplication.SharedApplication.SetAlternateIconName(iconName, (error) =>
        {
            tcs.SetResult(error == null);
        });

        return await tcs.Task;
    }
}
#endif
