using System.Text.Json;
using System.Text.RegularExpressions;

namespace kontur_hack2026.Services.Fakers;

public class FakerRegistry
{
    private Dictionary<string, string> _templates;
    private Random _random = new();

    public FakerRegistry(string path = "Services/Fakers/fakers.json")
    {
        var json = File.ReadAllText(path);
        _templates = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    public bool TryGenerate(string fakerKey, out string result)
    {
        if (!_templates.TryGetValue(fakerKey, out var template))
        {
            result = $"<unknown-faker:{fakerKey}>";
            return false;
        }
        result = Resolve(template);
        return true;
    }

    private string Resolve(string template)
    {
        template = Regex.Replace(template, @"\{random:(\d+)\}", m => 
            _random.Next(int.Parse(m.Groups[1].Value)).ToString());
        return template;
    }
}