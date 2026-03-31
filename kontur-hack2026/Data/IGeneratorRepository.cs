

using kontur_hack2026.Models;
using kontur_hack2026.Services;

namespace kontur_hack2026.Data;

public interface IGeneratorRepository
{
    public int Add(JsonSchemaNode node);
    public Generator Get(int id);
}