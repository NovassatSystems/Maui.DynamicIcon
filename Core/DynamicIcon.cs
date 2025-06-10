using System;

namespace Maui.DynamicIcon;

public static class DynamicIcon
{
    public static IDynamicIconService Current => _current ?? throw new InvalidOperationException("DynamicIconService not initialized.");
    static IDynamicIconService? _current;
    internal static void Initialize(IDynamicIconService service) => _current = service;
}
