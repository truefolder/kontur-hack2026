using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class ObjectGenerator(IGeneratorService generatorService) : ITypeGenerator<object>
{
    public object Generate(JsonSchemaNode node)
    {
        return generatorService.BuildObject(node.Properties);
    }

    object? ITypeGenerator.Generate(JsonSchemaNode node) => Generate(node);
}