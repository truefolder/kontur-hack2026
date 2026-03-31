using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public interface ITypeGenerator<out T> : ITypeGenerator
{
    new T Generate(JsonSchemaNode node);
}

public interface ITypeGenerator
{
    object? Generate(JsonSchemaNode node);
}