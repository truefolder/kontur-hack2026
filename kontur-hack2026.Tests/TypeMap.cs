using kontur_hack2026.Models;

namespace kontur_hack2026.Tests;

public static class TypeMap
{
    public static readonly Dictionary<string, Type> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(SupportedTypes.Integer), typeof(int) },
        { nameof(SupportedTypes.String), typeof(string) },
        { nameof(SupportedTypes.Boolean), typeof(bool) },
        { nameof(SupportedTypes.Float), typeof(float) },
        { nameof(SupportedTypes.DateTime), typeof(DateTime) }
    };

}