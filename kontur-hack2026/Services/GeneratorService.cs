using System.Dynamic;
using kontur_hack2026.Models;
using kontur_hack2026.Services.TypeGenerators;

namespace kontur_hack2026.Services;

public class GeneratorService : IGeneratorService
{
    private Random _rnd;

    private Dictionary<string, ITypeGenerator> _generators = new();
    private Dictionary<string, Func<string>> _fakerDict = new();

    public GeneratorService()
    {
        _rnd = new Random();
        _fakerDict.Add("internet.email", () => $"user{_rnd.Next(9999)}@example.com");

        _generators["string"] = new StringGenerator();
        _generators["integer"] = new IntGenerator();
        _generators["float"] = new FloatGenerator();
        _generators["boolean"] = new BoolGenerator();
        _generators["datetime"] = new DateTimeGenerator();
        _generators["object"] = new ObjectGenerator(this);
    }
    
    public dynamic GenerateFromSchema(JsonSchemaNode schema)
    {
        if (schema.Type != "object" || schema.Properties is null)
            throw new ArgumentException();

        return BuildObject(schema.Properties);
    }

    public ExpandoObject BuildObject(Dictionary<string, JsonSchemaNode> properties)
    {
        var obj = new ExpandoObject() as IDictionary<string, object?>;

        foreach (var (field, node) in properties)
            obj[field] = GenerateValue(node);

        return (ExpandoObject)obj;
    }
    
    private object? GenerateValue(JsonSchemaNode node)
    {
        if (node.Faker is not null)
        {
            if (_fakerDict.TryGetValue(node.Faker, out var faker))
                return faker();
        }
        return _generators[node.Type].Generate(node);
    }
}
