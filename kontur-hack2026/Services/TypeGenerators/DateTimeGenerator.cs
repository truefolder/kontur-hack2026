using kontur_hack2026.Models;

namespace kontur_hack2026.Services.TypeGenerators;

public class DateTimeGenerator : ITypeGenerator<DateTime>
{
    public DateTime Generate(JsonSchemaNode node)
    {
        var random = new Random();
        
        var start = new DateTime(1970, 1, 1);
        var range = (DateTime.Today - start).Days;           
        return start.AddDays(random.Next(range));
    }
    
    object? ITypeGenerator.Generate(JsonSchemaNode node) => Generate(node);
}