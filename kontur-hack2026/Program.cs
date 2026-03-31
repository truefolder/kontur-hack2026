using kontur_hack2026.Services;
using kontur_hack2026.Services.Fakers;
using kontur_hack2026.Data;
using kontur_hack2026.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GeneratorRepository>();
builder.Services.AddTransient<AppDbContext>();
builder.Services.AddTransient<IGeneratorService, GeneratorService>();
builder.Services.AddTransient<FakerRegistry>();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString,
        b => b.MigrationsAssembly("kontur-hack2026")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
