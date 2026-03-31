using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class ObjectGenerator(IGeneratorService generatorService) : TypeGeneratorBase<object>
{
    public override object Generate(JsonSchemaNode node)
    {
        return generatorService.BuildObject(node.Properties);
    }
}