using System.Text;
using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class StringGenerator : ITypeGenerator<string>
{
    public string Generate(JsonSchemaNode node)
    {
        var random = new Random();

        var length = random.Next(1, 10);
        var result = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var randomValue = random.Next(0, 26);
            result.Append(Convert.ToChar(randomValue + 65));
        }
        
        return result.ToString();
    }
    
    object? ITypeGenerator.Generate(JsonSchemaNode node) => Generate(node);
}