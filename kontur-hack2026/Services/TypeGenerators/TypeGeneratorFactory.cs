using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class TypeGeneratorFactory(Dictionary<string, ITypeGenerator> primitiveGenerators)
{
    public ITypeGenerator Create(JsonSchemaNode node)
    {
        if (node is null)
            throw new ArgumentNullException(nameof(node));

        if (node.Type.Equals(nameof(SupportedTypes.Dictionary), StringComparison.OrdinalIgnoreCase))
            return CreateDictionaryGenerator(node);

        if (node.Type.Equals(nameof(SupportedTypes.Array), StringComparison.OrdinalIgnoreCase))
            return CreateArrayGenerator(node);

        if (primitiveGenerators.TryGetValue(node.Type, out var generator))
            return generator;

        throw new NotSupportedException($"Unsupported type: {node.Type}");
    }
    
    private ITypeGenerator CreateArrayGenerator(JsonSchemaNode node)
    {
        if (node.Items is null)
            throw new ArgumentException("Array must contain items.");

        var itemGenerator = Create(node.Items);
        var itemType = itemGenerator.GeneratedType;

        var closedType = typeof(ArrayGenerator<>).MakeGenericType(itemType);

        return (ITypeGenerator)Activator.CreateInstance(
            closedType,
            itemGenerator
        )!;
    }

    private ITypeGenerator CreateDictionaryGenerator(JsonSchemaNode node)
    {
        if (node.Items is null)
            throw new ArgumentException("Dictionary must contain items.");

        var valueGenerator = Create(node.Items);
        var valueType = valueGenerator.GeneratedType;

        var closedType = typeof(DictionaryGenerator<>).MakeGenericType(valueType);

        return (ITypeGenerator)Activator.CreateInstance(
            closedType,
            valueGenerator
        )!;
    }
}