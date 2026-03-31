using kontur_hack2026.Data.Repositories;
using kontur_hack2026.Models;
using kontur_hack2026.Services;
using kontur_hack2026.Services.Fakers;

namespace kontur_hack2026.Tests;

[TestFixture]
public class CollectionTests
{   
    private IGeneratorService generator;
    
    public CollectionTests()
    {   
        var fakerRegistry = new FakerRegistry();
        generator = new GeneratorService(fakerRegistry);
    }
        
    [Test]
    public void Generate_ReturnsObject_WhenFieldTypeIsArray()
    {   
        var schema = new JsonSchemaNode
        {   
            Type = "object",
            Properties = new Dictionary<string, JsonSchemaNode>
            {
                ["Test"] = new()
                {
                    Type =  "Array",
                    Items = new()
                    {
                        Type = "string"
                    }
                }   
            }    
        };
        
        var result = (IDictionary<string, object>)generator.GenerateFromSchema(schema);
        
        Assert.That(result["Test"].GetType(), Is.EqualTo(typeof(List<string>)));

        var values = result.Values;
        foreach (var value in values)
        {
            foreach (var item in (List<string>)value)
            {
                Assert.That(item.GetType(), Is.EqualTo(typeof(string)));
            }
        }
    }
    
    [Test]
    public void Generate_ReturnsObject_WhenFieldTypeIsDictionary()
    {   
        var schema = new JsonSchemaNode
        {   
            Type = "object",
            Properties = new Dictionary<string, JsonSchemaNode>
            {
                ["Test"] = new()
                {
                    Type =  "Dictionary",
                    Items = new()
                    {
                        Type = "string"
                    }
                }   
            }    
        };
        
        var result = (IDictionary<string, object>)generator.GenerateFromSchema(schema);
        
        Assert.That(result["Test"].GetType(), Is.EqualTo(typeof(Dictionary<string,string>)));

        var values = result.Values;
        foreach (var value in values)
        {
            foreach (var item in (Dictionary<string,string>)value)
            {
                Assert.That(item.Value.GetType(), Is.EqualTo(typeof(string)));
            }
        }
    }
}