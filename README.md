![MADE WITH ❤️ BUILD WITH](https://img.shields.io/badge/Made_with_❤️_Build_with-000000?style=for-the-badge&labelColor=000000) 
![.NET MAUI](https://img.shields.io/badge/-MAUI-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) 
![C#](https://img.shields.io/badge/-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) 
![Android](https://img.shields.io/badge/-Android-3DDC84?style=for-the-badge&logo=android&logoColor=white) 
![iOS](https://img.shields.io/badge/-iOS-000000?style=for-the-badge&logo=apple&logoColor=white)

# Maui.DynamicIcon

**Maui.DynamicIcon** é um plugin leve e extensível para .NET MAUI que permite **trocar dinamicamente o ícone do app** em tempo de execução, utilizando `activity-alias` no Android e suporte completo no iOS.

---

## 📌 Suporte por plataforma

- ✅ **Android 21+**  
- ✅ **iOS** (totalmente suportado)  
- ⚠️ **Windows e MacCatalyst** não são suportados (ainda sem APIs equivalentes)

---

## 🚀 Instalação

### 1. Adicione o pacote NuGet ao seu projeto MAUI:
[![NuGet](https://img.shields.io/nuget/v/NovassatSystems.Maui.DynamicIcon.svg)](https://www.nuget.org/packages/NovassatSystems.Maui.DynamicIcon)

```bash
dotnet add package NovassatSystems.Maui.DynamicIcon
```

### 2. Ative o plugin no `MauiProgram.cs`, informando os aliases disponíveis:

```csharp
builder.UseDynamicIcon("Icon1", "Icon2", "Icon3");
```

---


## Configuração do Android  
![Android](https://img.shields.io/badge/-Android-3DDC84?style=for-the-badge&logo=android&logoColor=white)

1. **Crie os ícones alternativos**  
   Coloque os ícones desejados na pasta `Resources\Mipmap` com nomes diferentes (ex: `icon1.png`, `icon2.png`, etc).

2. **Crie `activity-alias` no `AndroidManifest.xml`**

Dentro da `<application>` do seu `AndroidManifest.xml`, adicione um `activity-alias` para cada ícone alternativo, apontando para sua `MainActivity`:

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

3. **Registre os aliases no `MauiProgram.cs`**

```csharp
builder.UseDynamicIcon("Icon1", "Icon2");
```

4. **Importante:** o nome usado no código deve ser o mesmo do atributo `android:name` (sem o ponto inicial se estiver no namespace raiz).

---

## Configuração do iOS  
![iOS](https://img.shields.io/badge/-iOS-000000?style=for-the-badge&logo=apple&logoColor=white)

1. **Crie múltiplos ícones no formato `.appiconset`**

Crie conjuntos de ícones separados com os nomes desejados, por exemplo:

```
Assets.xcassets/
├── AppIcon.appiconset (ícone padrão)
├── Icon1.appiconset
├── Icon2.appiconset
```

2. **Configure o `Info.plist`**

No seu `Info.plist`, adicione a chave `CFBundleIcons` com os ícones alternativos:

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

3. **Registre os aliases no `MauiProgram.cs`**

```csharp
builder.UseDynamicIcon("Icon1", "Icon2");
```

4. **Utilize normalmente com o plugin**

```csharp
await DynamicIcon.Current.SetIconAsync("Icon1");
```
---

## ✨ Exemplos de uso

### 🔧 1. Usando **injeção de dependência** com `DynamicIconManager`

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

> Útil quando você injeta o `DynamicIconManager` via DI no seu `ViewModel`, mantendo o controle do fluxo manualmente.

---

### ♻️ 2. Usando **Strategy Pattern** com `DynamicIconManager`

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

> Ideal para cenários com regras complexas de alternância ou personalização futura da lógica de troca de ícone.

---

### ⚡ 3. Acesso direto com `DynamicIcon.Current` (estático)

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

> Alternativa rápida para uso direto sem injeção de dependência — útil em projetos simples ou em `code-behind`.

---

## 🛠️ Considerações no modo Debug (Android)

Ao ativar um alias, o plugin *desativa* a `MainActivity`, o que pode causar falha no deploy pelo Visual Studio com o erro:

```text
Error: Activity class ...MainActivity does not exist.
```

**Possíveis soluções:**

- Chame `_iconManager.ResetToDefault();` antes de iniciar o app  
- Ou reative manualmente via ADB:

```bash
adb shell pm enable seu.pacote/.MainActivity
```

---

## 📂 Projeto de exemplo

Confira o projeto `Maui.DynamicIcon.Sample` no repositório para testar e experimentar as funcionalidades em tempo real.

---

## ⚙️ Demonstração

*Alternância de ícones em tempo real – trabalhando em um gif para ilustrar em breve.*  

*(Placeholder para link ou GIF)*

---

## 🧰 Tecnologias utilizadas

- [​.NET MAUI](https://learn.microsoft.com/dotnet/maui/what-is-maui)  
- [C#](https://docs.microsoft.com/dotnet/csharp/)  
- [Android – Activity Alias](https://developer.android.com/guide/topics/manifest/activity-alias-element)  
- iOS – APIs de troca de ícone em tempo de execução

---

## 🤝 Contribuindo

Contribuições são super bem-vindas! Abra issues, envie pull requests ou dê sugestões nas Discussions.

---

## 📄 Licença

Este projeto é licenciado sob **MIT**.

---

## 📞 Contato

Se tiver dúvidas ou sugestões:  
- 🔗 [LinkedIn – Peter Novassat](https://www.linkedin.com/in/PeterNovassat)  
- 🐞 Abra uma *issue* diretamente no GitHub

---
![GitHub repo size](https://img.shields.io/github/repo-size/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub issues](https://img.shields.io/github/issues/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
![GitHub pull requests](https://img.shields.io/github/issues-pr/NovassatSystems/Maui.DynamicIcon?style=for-the-badge)
