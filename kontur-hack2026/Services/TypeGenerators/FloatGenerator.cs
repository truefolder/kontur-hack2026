using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class FloatGenerator : ITypeGenerator<float>
{
    public float Generate(JsonSchemaNode node)
    {
        var random = new Random();

        return MathF.Round((float)random.NextDouble() * 1000, 2);
    }

    object? ITypeGenerator.Generate(JsonSchemaNode node) => Generate(node);
}