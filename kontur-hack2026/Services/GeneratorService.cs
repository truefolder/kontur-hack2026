using System.Dynamic;
using kontur_hack2026.Models;
using kontur_hack2026.Services.Fakers;
using kontur_hack2026.Services.TypeGenerators;

namespace kontur_hack2026.Services;

public class GeneratorService : IGeneratorService
{
    private Random _rnd;

    private Dictionary<string, ITypeGenerator> _generators = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, Func<string>> _fakerDict = new();
    private FakerRegistry _fakerRegistry;

    public GeneratorService(FakerRegistry fakerRegistry)
    {
        _fakerRegistry = fakerRegistry;
        _rnd = new Random();
        _fakerDict.Add("internet.email", () => $"user{_rnd.Next(9999)}@example.com");

        _generators[nameof(SupportedTypes.String)] = new StringGenerator();
        _generators[nameof(SupportedTypes.Integer)] = new IntGenerator();
        _generators[nameof(SupportedTypes.Float)] = new FloatGenerator();
        _generators[nameof(SupportedTypes.Boolean)] = new BoolGenerator();
        _generators[nameof(SupportedTypes.DateTime)] = new DateTimeGenerator();
        _generators[nameof(SupportedTypes.Object)] = new ObjectGenerator(this);
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
            if (_fakerRegistry.TryGenerate(node.Faker, out var faker))
                return faker;
        }
        return _generators[node.Type].Generate(node);
    }
}
