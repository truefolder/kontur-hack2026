using System.Dynamic;
using kontur_hack2026.Models;

namespace kontur_hack2026.Services;

public class GeneratorService : IGeneratorService
{
    private Random _rnd;

    private Dictionary<string, Func<string>> _fakerDict = new();
    public GeneratorService()
    {
        _rnd = new Random();
        _fakerDict.Add("internet.email", () => $"user{_rnd.Next(9999)}@example.com");
    }
    
    public dynamic GenerateFromSchema(JsonSchemaNode schema)
    {
        if (schema.Type != "object" || schema.Properties is null)
            throw new ArgumentException();

        return BuildObject(schema.Properties);
    }

    private ExpandoObject BuildObject(Dictionary<string, JsonSchemaNode> properties)
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
        return node.Type switch
        {
            "string"  => "hello world",
            "integer" => _rnd.Next(1, 10000),
            "number"  => Math.Round(_rnd.NextDouble() * 1000, 2),
            "boolean" => _rnd.Next(2) == 1,
            _         => null
        };
    }
}