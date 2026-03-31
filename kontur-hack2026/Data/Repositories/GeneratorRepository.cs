using kontur_hack2026.Data.Entitties;

namespace kontur_hack2026.Data.Repositories;

public class GeneratorRepository: Repository<GeneratorEntity>
{
    public GeneratorRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
    public GeneratorEntity? GetById(Guid Id)
    {
        return Get().SingleOrDefault(x => x.Id == Id);
    }
}