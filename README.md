![Made with](https://img.shields.io/badge/Made_with-%20-ffffff?style=for-the-badge&labelColor=ffffff)
![.NET MAUI](https://img.shields.io/badge/-MAUI-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Android](https://img.shields.io/badge/-Android-3DDC84?style=for-the-badge&logo=android&logoColor=white)
[![NuGet](https://img.shields.io/nuget/v/Novassat.DynamicIcon.svg)](https://www.nuget.org/packages/Novassat.DynamicIcon)



# Maui.DynamicIcon

**Maui.DynamicIcon** é um plugin leve e extensível para .NET MAUI que permite trocar dinamicamente o ícone do aplicativo no Android em tempo de execução, utilizando `activity-alias`.

> ✅ Compatível com Android 21+  
> ⚠️ Atualmente funciona apenas no Android. Suporte para iOS está em desenvolvimento. Windows e MacCatalyst não são suportados (não possuem APIs equivalentes).

---

## 🚀 Instalação

1. Adicione o pacote NuGet ao seu projeto Android MAUI:

   ```bash
   dotnet add package Maui.DynamicIcon
   ```

2. No seu `MauiProgram.cs`, registre o plugin informando os aliases disponíveis:

   ```csharp
   builder.UseDynamicIcon<MainActivity>(
       "Icon1Alias", "Icon2Alias", "Icon3Alias"
   );
   ```

---

## ⚙️ Configuração do AndroidManifest.xml

Garanta que sua `MainActivity` e os `activity-alias` estejam definidos no `AndroidManifest.xml`:

```xml
<application ...>
    <activity android:name=".MainActivity"
              android:exported="true"
              android:theme="@style/Maui.SplashTheme"
              android:launchMode="singleTop"
              android:configChanges="...">
        <intent-filter>
            <action android:name="android.intent.action.MAIN" />
            <category android:name="android.intent.category.LAUNCHER" />
        </intent-filter>
    </activity>

    <activity-alias
        android:name=".Icon1Alias"
        android:enabled="false"
        android:exported="true"
        android:icon="@mipmap/icon1"
        android:targetActivity=".MainActivity">
        <intent-filter>
            <action android:name="android.intent.action.MAIN" />
            <category android:name="android.intent.category.LAUNCHER" />
        </intent-filter>
    </activity-alias>

    <!-- Adicione outros aliases conforme necessário -->
</application>
```

---

## ✨ Como usar

### ✅ Trocar ícone de forma simples (ex: clique de botão)

```csharp
_iconManager.SetAliasOnce("Icon2Alias");
```

### 🔁 Voltar para o ícone padrão (MainActivity)

```csharp
_iconManager.ResetToDefault();
```

---

## 🧠 Troca com estratégia personalizada

Implemente `IDynamicIconStrategy` com a lógica de troca:

```csharp
public class AlternatingIconStrategy : IDynamicIconStrategy
{
    private int _counter = 0;

    public string? GetNextIconAlias()
    {
        _counter++;
        return _counter % 2 == 0 ? "Icon1Alias" : "Icon2Alias";
    }
}
```

E aplique no gerenciador:

```csharp
_iconManager.SetStrategy(new AlternatingIconStrategy());
_iconManager.ApplyStrategy();
```

---

## 🧪 Importante no modo de desenvolvimento (Debug)

Ao ativar um alias, o plugin desativa a `MainActivity`.  
⚠️ Isso pode causar erro ao fazer deploy via Visual Studio:

```
Error: Activity class ...MainActivity does not exist.
```

### 🔧 Soluções:

- Chame `_iconManager.ResetToDefault();` antes de executar o app
- Ou reative manualmente a MainActivity via ADB:

```bash
adb shell pm enable seu.pacote/.MainActivity
```

---

## 📱 Projeto de Exemplo

Veja o projeto `Maui.DynamicIcon.Sample` incluído no repositório para testar em tempo real.

---

## 📄 Licença

MIT

---

Feito com ❤️ para a comunidade MAUI.

---

## 🧩 Sobre o projeto

O `Maui.DynamicIcon` é um plugin open-source feito para facilitar a troca dinâmica de ícones no Android usando .NET MAUI.  
Ideal para apps com múltiplas identidades visuais, temas ou funcionalidades contextuais.

---

## 📷 Demonstração

| Alternância de ícones em tempo real |
|-------------------------------------|
| (Trabalhando nisso hehe.(Mas CONFIA, funciona!) |

---

## 🛠️ Tecnologias Utilizadas

- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui)
- [C#](https://docs.microsoft.com/dotnet/csharp/)
- [Android - Activity Alias](https://developer.android.com/guide/topics/manifest/activity-alias-element)

---

## 🤝 Contribuindo

Contribuições são bem-vindas!  
Sinta-se à vontade para abrir issues, enviar pull requests ou sugerir melhorias via discussions.

---

## 📬 Contato

Tem dúvidas ou sugestões?  
Entre em contato comigo por [LinkedIn](https://www.linkedin.com) ou abra uma issue no repositório.

---

