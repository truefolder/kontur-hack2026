using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public abstract class TypeGeneratorBase<T> : ITypeGenerator<T>
{
    public Type GeneratedType => typeof(T);
    public abstract T Generate(JsonSchemaNode node);
    object? ITypeGenerator.Generate(JsonSchemaNode node) => Generate(node);
}