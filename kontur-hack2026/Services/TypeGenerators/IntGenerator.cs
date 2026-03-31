using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class IntGenerator : ITypeGenerator<int>
{
    public int Generate(JsonSchemaNode node)
    {
        var random = new Random();
        
        return random.Next(1, 999999);
    }
    
    object? ITypeGenerator.Generate(JsonSchemaNode node) => Generate(node);
}