using System.Dynamic;
using System.Text.Json.Serialization;

namespace kontur_hack2026.Models;

public class JsonSchemaNode
{
    [JsonPropertyName("type")] 
    public string Type { get; set; } = "string";

    [JsonPropertyName("properties")]
    public Dictionary<string, JsonSchemaNode>? Properties { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; }
    
    [JsonPropertyName("faker")]
    public string? Faker { get; set; }
}