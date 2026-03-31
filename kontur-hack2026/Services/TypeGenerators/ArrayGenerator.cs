using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class ArrayGenerator<T>(ITypeGenerator itemGenerator) : TypeGeneratorBase<List<T>>
{
    public override List<T> Generate(JsonSchemaNode node)
    {
        if (node.Items == null)
            throw new ArgumentException("Array must contain items");

        var random = new Random();
        
        var count = random.Next(1, 10);
        var result = new List<T>();

        for (var i = 0; i < count; i++)
        {
            var value = itemGenerator.Generate(node.Items);
            result.Add((T)value!);
        }

        return result;
    }
}