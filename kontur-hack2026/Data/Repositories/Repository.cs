using kontur_hack2026.Data.Entitties;
using Microsoft.EntityFrameworkCore;

namespace kontur_hack2026.Data.Repositories;

public abstract class Repository<TEntity> where TEntity: Entity
{
    private readonly ApplicationContext applicationContext;

    public Repository(ApplicationContext applicationContext)
    {
        this.applicationContext = applicationContext;
    }
    
    public void Save(TEntity entity)
    {
        if (Get().Any(x => x.Id == entity.Id))
            applicationContext.Update(entity);
        else
            applicationContext.Add(entity);
        
        applicationContext.SaveChanges();
    }

    public IQueryable<TEntity> Get()
    {
        return applicationContext.Set<TEntity>().AsNoTracking();
    }
}