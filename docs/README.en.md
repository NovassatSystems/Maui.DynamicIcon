📘 Este README também está disponível em: [🇧🇷 Portuguese](../README.md)

![MADE WITH LOVE](https://img.shields.io/badge/Made_with_Love-Build_with-000000?style=for-the-badge) 
![.NET MAUI](https://img.shields.io/badge/-MAUI-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) 
![C#](https://img.shields.io/badge/-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) 
![Android](https://img.shields.io/badge/-Android-3DDC84?style=for-the-badge&logo=android&logoColor=white) 
![iOS](https://img.shields.io/badge/-iOS-000000?style=for-the-badge&logo=apple&logoColor=white)
[![NuGet](https://img.shields.io/nuget/v/NovassatSystems.Maui.DynamicIcon.svg)](https://www.nuget.org/packages/NovassatSystems.Maui.DynamicIcon)

# Maui.DynamicIcon

**Maui.DynamicIcon** is a lightweight and extensible plugin for .NET MAUI that allows you to **dynamically change the app icon** at runtime, using `activity-alias` on Android and full support on iOS.

---

## 📌 Platform Support

- ✅ **Android 21+**  
- ✅ **iOS** (fully supported)  
- ⚠️ **Windows and MacCatalyst** are not supported (no available APIs yet)

---

## 🚀 Installation

### 1. Add the NuGet package to your MAUI project:
[![NuGet](https://img.shields.io/nuget/v/NovassatSystems.Maui.DynamicIcon.svg)](https://www.nuget.org/packages/NovassatSystems.Maui.DynamicIcon)

```bash
dotnet add package NovassatSystems.Maui.DynamicIcon
```

### 2. Enable the plugin in `MauiProgram.cs`, listing the available aliases:

```csharp
builder.UseDynamicIcon("Icon1Alias", "Icon2Alias", "Icon3Alias");
```

---

## Android Setup  
![Android](https://img.shields.io/badge/-Android-3DDC84?style=for-the-badge&logo=android&logoColor=white)

1. **Create the alternate icons**  
   Place your desired icons in the `Resources\Mipmap` folder with different names (e.g., `icon1.png`, `icon2.png`, etc).

2. **Add `activity-alias` to `AndroidManifest.xml`**

Inside the `<application>` tag of your `AndroidManifest.xml`, add an `activity-alias` for each alternate icon, targeting your `MainActivity`:

```xml
<activity-alias
    android:name=".Icon1"
    android:enabled="false"
    android:exported="true"
    android:icon="@mipmap/icon1"
    android:targetActivity=".MainActivity">
    <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
    </intent-filter>
</activity-alias>

<activity-alias
    android:name=".Icon2"
    android:enabled="false"
    android:exported="true"
    android:icon="@mipmap/icon2"
    android:targetActivity=".MainActivity">
    <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
    </intent-filter>
</activity-alias>
```

3. **Register aliases in `MauiProgram.cs`**

```csharp
builder.UseDynamicIcon("Icon1", "Icon2");
```

4. **Important:** the name used in code must match the `android:name` attribute (without the leading dot if in the root namespace).

---

## iOS Setup  
![iOS](https://img.shields.io/badge/-iOS-000000?style=for-the-badge&logo=apple&logoColor=white)

1. **Create multiple icons in `.appiconset` format**

Create separate icon sets with the desired names, for example:

```
Assets.xcassets/
├── AppIcon.appiconset (default icon)
├── Icon1.appiconset
├── Icon2.appiconset
```

2. **Configure `Info.plist`**

In your `Info.plist`, add the `CFBundleIcons` key with the alternate icons:

```xml
<key>CFBundleIcons</key>
<dict>
  <key>CFBundleAlternateIcons</key>
  <dict>
    <key>Icon1</key>
    <dict>
      <key>CFBundleIconFiles</key>
      <array>
        <string>Icon1</string>
      </array>
      <key>UIPrerenderedIcon</key>
      <false/>
    </dict>
    <key>Icon2</key>
    <dict>
      <key>CFBundleIconFiles</key>
      <array>
        <string>Icon2</string>
      </array>
      <key>UIPrerenderedIcon</key>
      <false/>
    </dict>
  </dict>
</dict>
```

3. **Register aliases in `MauiProgram.cs`**

```csharp
builder.UseDynamicIcon("Icon1", "Icon2");
```

4. **Use normally with the plugin**

```csharp
await DynamicIcon.Current.SetIconAsync("Icon1");
```

---

## ✨ Usage Examples

### 🔧 1. Using **dependency injection** with `DynamicIconManager`

```csharp
public class MainViewModel(DynamicIconManager iconManager)
{
    readonly DynamicIconManager _iconManager = iconManager;
    int _counter = 0;

    public async void ToggleIconWithDynamicIconManager()
    {
        var alias = _counter++ % 2 == 0 ? "Icon1" : "Icon2";
        await _iconManager.SetAliasOnceAsync(alias);
    }
}
```

> Useful when injecting `DynamicIconManager` via DI in your `ViewModel`, maintaining full control manually.

---

### ♻️ 2. Using **Strategy Pattern** with `DynamicIconManager`

```csharp
public class MainViewModel(DynamicIconManager iconManager)
{
    readonly DynamicIconManager _iconManager = iconManager;

    public async void ToggleIconWithDynamicIconManagerAndStrategy()
    {
        _iconManager.SetStrategy(new SingleUseStrategy(true));
        await _iconManager.ApplyStrategyAsync();
    }
}

public class SingleUseStrategy(bool mustBeChange) : IDynamicIconStrategy
{
    private readonly string _alias = mustBeChange ? "Icon1" : "Icon2";
    public string? GetNextIconAlias() => _alias;
}
```

> Great for scenarios with complex switching logic or reusable behavior.

---

### ⚡ 3. Direct access with `DynamicIcon.Current` (static)

```csharp
public class MainViewModel
{
    int _counter = 0;

    public async Task ToggleIconWithDynamicIconCurrent()
    {
        var alias = _counter++ % 2 == 0 ? "Icon1" : "Icon2";
        await DynamicIcon.Current.SetIconAsync(alias);
    }
}
```

> Fast alternative when DI isn't needed — ideal for quick demos or code-behind use.

---

## 🛠️ Debug Considerations (Android)

Activating an alias disables the `MainActivity`, which can cause Visual Studio deployment errors like:

```text
Error: Activity class ...MainActivity does not exist.
```

**Possible solutions:**

- Call `_iconManager.ResetToDefault();` before launching the app  
- Or manually re-enable via ADB:

```bash
adb shell pm enable your.package/.MainActivity
```

---

## 📂 Sample Project

Check out the `Maui.DynamicIcon.Sample` project in the repository to test and explore real usage.

---

## ⚙️ Demo

*Icon switching in real time — a GIF will be added soon.*

---

## 🧰 Technologies Used

- [​.NET MAUI](https://learn.microsoft.com/dotnet/maui/what-is-maui)  
- [C#](https://docs.microsoft.com/dotnet/csharp/)  
- [Android – Activity Alias](https://developer.android.com/guide/topics/manifest/activity-alias-element)  
- iOS – Runtime icon switching APIs

---

## 🤝 Contributing

All contributions are welcome! Open issues, submit pull requests, or start a Discussion.

---

## 📄 License

This project is licensed under the **MIT** License.

---

## 📞 Contact

If you have questions or suggestions:  
- 🔗 [LinkedIn – Peter Novassat](https://www.linkedin.com/in/PeterNovassat)  
- 🐞 Open an *issue* directly on GitHub

---

![GitHub repo size](https://img.shields.io/github/repo-size/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub issues](https://img.shields.io/github/issues/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub pull requests](https://img.shields.io/github/issues-pr/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)