using kontur_hack2026.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace kontur_hack2026.Data;

public class AppDbContext : DbContext
{
    public DbSet<GeneratorEntity> Generators { get; set; } = null!;

    public AppDbContext()
    {
        Database.EnsureCreated();
    }
}