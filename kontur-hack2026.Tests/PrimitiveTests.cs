using kontur_hack2026.Models;
using kontur_hack2026.Services;

namespace kontur_hack2026.Tests;

[TestFixture]
public class PrimitiveTests
{   
    private readonly IGeneratorService generator;
    
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
        var expectedType = TypeMap.Map[fieldType];
        
        var result = (IDictionary<string, object>)generator.GenerateFromSchema(schema);
        
        Assert.That(result["Test"].GetType(), Is.EqualTo(expectedType));
    }
    
    [Test]
    public void Generate_ReturnsObject_WhenPropertyIsEmpty()
    {   
        var schema = new JsonSchemaNode
        {   
            Type = "object",
            Properties = new Dictionary<string, JsonSchemaNode>
            {
            }
        };
        
        var result = (IDictionary<string, object>)generator.GenerateFromSchema(schema);
        
        Assert.That(result.Values, Is.Empty);
    }
    
    [TestCase(" ")]
    public void Generate_ThrowException_WhenFieldTypeIsNotSupported(string fieldType)
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
    
    [Test]
    public void Generate_ThrowArgumentException_WhenPropertiesIsNull()
    {
        var schema = new JsonSchemaNode
        {
            Type = "object"
        };
        
        Assert.Throws<ArgumentException>(() => { generator.GenerateFromSchema(schema); });
    }
    
    [Test]
    public void Generate_ThrowArgumentException_WhenTypeIsNotValid()
    {
        var schema = new JsonSchemaNode
        {
            Type = "123"
        };
        
        Assert.Throws<ArgumentException>(() => { generator.GenerateFromSchema(schema); });
    }
}


