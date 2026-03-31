using kontur_hack2026.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace kontur_hack2026.Data;

public class ApplicationContext : DbContext
{
    public DbSet<GeneratorEntity> Generators { get; set; } = null!;

    public ApplicationContext()
    {
        this.Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConnectionString
        {
            DatabaseName = $"POSTGRES_DB",
            Host = "postgres",
            Port = "5431",
            Password =  $"POSTGRES_PASSWORD",
            UserName = $"POSTGRES_USER"
        };
        optionsBuilder.UseNpgsql(connectionString.Build());
    }
}