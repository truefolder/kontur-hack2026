using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Npgsql.TypeMapping;

namespace kontur_hack2026.Models;

public class Generator(JsonSchemaNode node)
{
    public JsonSchemaNode node { get; set; } = node;
}