using kontur_hack2026.Data.Entities;

namespace kontur_hack2026.Data.Repositories;

public class GeneratorRepository: Repository<GeneratorEntity>
{
    public GeneratorRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
    public GeneratorEntity? GetById(Guid Id)
    {
        return Get().SingleOrDefault(x => x.Id == Id);
    }
}