using System.Dynamic;
using kontur_hack2026.Models;

namespace kontur_hack2026.Services;

public interface IGeneratorService
{
    public dynamic GenerateFromSchema(JsonSchemaNode schema);
    
    public ExpandoObject BuildObject(Dictionary<string, JsonSchemaNode> properties);

    public Guid Save(JsonSchemaNode node);

    public Generator GetById(Guid id);

    //void Update(UserEntity user);
    //void UpdateOrInsert(UserEntity user, out bool isInserted);
    //void Delete(Guid id);


}