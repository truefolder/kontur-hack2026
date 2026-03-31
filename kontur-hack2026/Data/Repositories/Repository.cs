using kontur_hack2026.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace kontur_hack2026.Data.Repositories;

public abstract class Repository<TEntity> where TEntity: Entity
{
    private readonly AppDbContext _appDbContext;

    public Repository(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }
    
    public Guid Save(TEntity entity)
    {
        if (Get().Any(x => x.Id == entity.Id))
            _appDbContext.Update(entity);
        else
            _appDbContext.Add(entity);
        
        _appDbContext.SaveChanges();
        return entity.Id;
    }

    public IQueryable<TEntity> Get()
    {
        return _appDbContext.Set<TEntity>().AsNoTracking();
    }
}