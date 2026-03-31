using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class BoolGenerator : TypeGeneratorBase<bool>
{
    public override bool Generate(JsonSchemaNode node)
    {
        var random = new Random();
        
        return random.NextDouble() >= 0.5;
    }
}