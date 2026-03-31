using kontur_hack2026.Services;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Npgsql.TypeMapping;

namespace kontur_hack2026.Models;

public class Generator(IGeneratorService generatorService, JsonSchemaNode node)
{
    public JsonSchemaNode node { get; set; } = node;
    public IGeneratorService generatorService { get; set; } = generatorService;
}