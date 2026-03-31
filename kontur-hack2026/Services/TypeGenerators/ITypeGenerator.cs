using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public interface ITypeGenerator<out T> : ITypeGenerator
{
    public new T Generate(JsonSchemaNode node);
}

public interface ITypeGenerator
{
    public object? Generate(JsonSchemaNode node);
    public Type GeneratedType { get; }
}