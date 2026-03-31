using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class DictionaryGenerator<T>(ITypeGenerator typeGenerator) : TypeGeneratorBase<Dictionary<string, T>>
{
    public override Dictionary<string, T> Generate(JsonSchemaNode node)
    {
        if (node.Items is null)
            throw new ArgumentException("Dictionary schema must contain items.");
        
        var random = new Random();
        
        var count = random.Next(1, 10);
        var result = new Dictionary<string, T>(count);

        for (var i = 0; i < count; i++)
        {
            var key = $"key{i + 1}";
            var value = typeGenerator.Generate(node.Items);
            result[key] = (T)value!;
        }

        return result;
    }
}