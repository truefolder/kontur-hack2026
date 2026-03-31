using kontur_hack2026.Models;
using kontur_hack2026.Services;

namespace kontur_hack2026.Tests;

public class PrimitiveTests
{   
    private IGeneratorService generator;
    
    private Dictionary<string, Type> typeMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(SupportedTypes.Integer), typeof(int) },
        { nameof(SupportedTypes.String), typeof(string) },
        { nameof(SupportedTypes.Boolean), typeof(bool) },
        { nameof(SupportedTypes.Float), typeof(float) },
        { nameof(SupportedTypes.DateTime), typeof(DateTime) }
    };
    
    
    public PrimitiveTests()
    {
        generator = new GeneratorService();
    }
    
    [TestCase(nameof(SupportedTypes.Integer))]
    [TestCase(nameof(SupportedTypes.Float))]
    [TestCase(nameof(SupportedTypes.String))]
    [TestCase(nameof(SupportedTypes.Boolean))]
    [TestCase(nameof(SupportedTypes.DateTime))]
    public void Generate_ReturnsInputFieldType_WhenFieldTypeIsSupported(string fieldType)
    {   
        var schema = new JsonSchemaNode
        {   
            Type = "object",
            Properties = new Dictionary<string, JsonSchemaNode>
            {
                ["Test"] = new() { Type = fieldType }
            }
        };
        var expectedType = typeMap[fieldType];
        
        var result = (IDictionary<string, object>)generator.GenerateFromSchema(schema);
        
        Assert.That(result["Test"].GetType(), Is.EqualTo(expectedType));
    }

    [TestCase(" ")]
    public void Generate_ReturnsFailureMessage_WhenFieldTypeIsNotSupported(string fieldType)
    {
        var schema = new JsonSchemaNode
        {
            Type = "object",
            Properties = new Dictionary<string, JsonSchemaNode>
            {
                ["Test"] = new() { Type = fieldType }
            }
        };
        
        Assert.Throws<KeyNotFoundException>(() => { generator.GenerateFromSchema(schema); });
    }
}