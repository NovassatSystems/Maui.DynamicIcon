using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;


namespace Maui.DynamicIcon.Sample.Platforms.Android.Services;

[Service(Exported = true)]
public class IconChangerService : Service
{
    private int _currentIndex = 0;
    private string[] _aliases = { "Icon1Alias", "Icon2Alias", "Icon3Alias" };
    private Timer? _timer;

    public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
    {
        ShowNotification();

        _timer = new Timer(SwapIcon, null, 0, 5000); // 5 segundos
        return StartCommandResult.Sticky;
    }

    private void SwapIcon(object? state)
    {
        var pm = ApplicationContext.PackageManager;

        for (int i = 0; i < _aliases.Length; i++)
        {
            var alias = new ComponentName(ApplicationContext, $"{ApplicationContext.PackageName}.{_aliases[i]}");

            var stateToSet = i == _currentIndex
                ? ComponentEnabledState.Enabled
                : ComponentEnabledState.Disabled;

            pm.SetComponentEnabledSetting(
                alias,
                stateToSet,
                ComponentEnableOption.DontKillApp);
        }

        _currentIndex = (_currentIndex + 1) % _aliases.Length;
    }

    private void ShowNotification()
    {
        var channelId = "icon_changer";
        var notification = new NotificationCompat.Builder(this, channelId)
            .SetContentTitle("Trocando ícones")
            .SetContentText("O serviço de troca de ícones está rodando")
            .SetSmallIcon(Microsoft.Maui.Controls.Resource.Drawable.ic_launcher)
            .Build();

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(channelId, "Troca de Ícones", NotificationImportance.Low);
            var manager = (NotificationManager)GetSystemService(NotificationService);
            manager.CreateNotificationChannel(channel);
        }

        StartForeground(1, notification);
    }

    public override IBinder? OnBind(Intent intent) => null;

    public override void OnDestroy()
    {
        _timer?.Dispose();
        base.OnDestroy();
    }
}
