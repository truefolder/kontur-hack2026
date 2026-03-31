using kontur_hack2026.Models;
using kontur_hack2026.Services;

namespace kontur_hack2026.Tests;

[TestFixture]
public class NestedObjectTests
{
    private IGeneratorService generator;
    
    public NestedObjectTests()
    {
        generator = new GeneratorService();
    }

    [Test]
    public void Generate_ReturnsNestedObject_WhenFieldTypeIsObject()
    {
        var schema = new JsonSchemaNode
        {   
            Type = "object",
            Properties = new Dictionary<string, JsonSchemaNode>
            {
                ["Test"] = new()
                {
                    Type =  "object",
                    Properties = new Dictionary<string, JsonSchemaNode>
                    {
                        ["NestedObject"] = new()
                        {
                            Type = "string",
                        }
                    }
                }   
            }    
        };
        
        var result = (IDictionary<string, object>)generator.GenerateFromSchema(schema);
        
        AssertProperties(result, schema.Properties);
    }
    
    private void AssertProperties(IDictionary<string, object> obj, Dictionary<string, JsonSchemaNode> properties)
    {
        foreach (var (key, value) in properties)
        {
            Assert.That(obj.ContainsKey(key), Is.True);
                
            if (value.Type == "object")
            {
                var nestedObject = (IDictionary<string, object>)obj[key];
                if (value.Properties != null) AssertProperties(nestedObject, value.Properties);
            }
            else
            {
                var expectedType = TypeMap.Map[value.Type]; 
                Assert.That(obj[key].GetType(), Is.EqualTo(expectedType));
            }
        }
    }
}

