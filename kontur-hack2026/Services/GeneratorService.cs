using System.Dynamic;
using kontur_hack2026.Data;
using kontur_hack2026.Data.Repositories;
using kontur_hack2026.Data.Entitties;
using kontur_hack2026.Models;
using kontur_hack2026.Services.Fakers;
using kontur_hack2026.Services.TypeGenerators;

namespace kontur_hack2026.Services;

public class GeneratorService : IGeneratorService
{
    private Random _rnd;

    private Dictionary<string, ITypeGenerator> _primitiveGenerators = new(StringComparer.OrdinalIgnoreCase);
    private FakerRegistry _fakerRegistry;
    private TypeGeneratorFactory _typeGeneratorFactory;
    private GeneratorRepository generatorRepository;

    public GeneratorService(FakerRegistry fakerRegistry, GeneratorRepository generatorRepository)
    {
        _fakerRegistry = fakerRegistry;
        _rnd = new Random();

        _primitiveGenerators[nameof(SupportedTypes.String)] = new StringGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Integer)] = new IntGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Float)] = new FloatGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Boolean)] = new BoolGenerator();
        _primitiveGenerators[nameof(SupportedTypes.DateTime)] = new DateTimeGenerator();
        _primitiveGenerators[nameof(SupportedTypes.Object)] = new ObjectGenerator(this);
        
        _typeGeneratorFactory = new(_primitiveGenerators);
        this.generatorRepository = generatorRepository;
    }

    public GeneratorService()
    {
        throw new NotImplementedException();
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

    public Guid Save(JsonSchemaNode node)
    {
        return generatorRepository.Save(new GeneratorEntity { node= node});
    }
    public Generator GetById(Guid id)
    {
        return new Generator(generatorRepository.GetById(id).node);
    }

}
