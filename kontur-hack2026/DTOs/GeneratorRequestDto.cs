using System.Text.Json.Serialization;
using kontur_hack2026.Models;

namespace kontur_hack2026.DTOs;

public class GeneratorRequestDto
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