using System.Dynamic;
using kontur_hack2026.Models;

namespace kontur_hack2026.Services;

public interface IGeneratorService
{
    public dynamic GenerateFromSchema(JsonSchemaNode schema);
    
    public ExpandoObject BuildObject(Dictionary<string, JsonSchemaNode> properties);
}