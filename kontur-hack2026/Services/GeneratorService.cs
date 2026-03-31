using System.Dynamic;
using kontur_hack2026.Models;
using kontur_hack2026.Services.Fakers;
using kontur_hack2026.Services.TypeGenerators;

namespace kontur_hack2026.Services;

public class GeneratorService : IGeneratorService
{
    private Random _rnd;

    private Dictionary<string, ITypeGenerator> _primitiveGenerators = new(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, Func<string>> _fakerDict = new();
    private FakerRegistry _fakerRegistry;
    private TypeGeneratorFactory _typeGeneratorFactory;

    public GeneratorService(FakerRegistry fakerRegistry)
    {
        _fakerRegistry = fakerRegistry;
        _rnd = new Random();
        _fakerDict.Add("internet.email", () => $"user{_rnd.Next(9999)}@example.com");

        _primitiveGenerators[nameof(SupportedTypes.String)] = new StringGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Integer)] = new IntGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Float)] = new FloatGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Boolean)] = new BoolGenerator();
        _primitiveGenerators[nameof(SupportedTypes.DateTime)] = new DateTimeGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Object)] = new ObjectGenerator(this);
        
        _typeGeneratorFactory = new(_primitiveGenerators);
    }
    
    public dynamic GenerateFromSchema(JsonSchemaNode schema)
    {
        if (schema.Type != "object")
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

        var generator = _typeGeneratorFactory.Create(node);
        return generator.Generate(node);
    }
}
