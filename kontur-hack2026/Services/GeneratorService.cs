using System.Dynamic;
using kontur_hack2026.Models;

namespace kontur_hack2026.Services;

public class GeneratorService : IGeneratorService
{
    private Random _rnd;
    public GeneratorService()
    {
        _rnd = new Random();
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