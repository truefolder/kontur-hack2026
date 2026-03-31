using System.Dynamic;
using System.Xml.Linq;
using kontur_hack2026.Data;
using kontur_hack2026.Models;
using kontur_hack2026.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql.TypeMapping;

namespace kontur_hack2026.Controllers;

[ApiController]
[Route("generators")]
public class GeneratorsContoller(IGeneratorService generatorService)
{
    [HttpPost]
    public ActionResult<Guid> CreateGenerator([FromBody] JsonSchemaNode node)
    {
        return generatorService.Save(node);
    }

    [HttpGet]
    public ActionResult<ExpandoObject> GetGenerators()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public ActionResult<JsonSchemaNode> GetGenerator(Guid id)
    {
        var generator = generatorService.GetById(id);
        return generator.node;
    }

    [HttpGet("{id}/generate")]
    public ActionResult<ExpandoObject> GenerateJson(Guid id)
    {
        var generator = generatorService.GetById(id);
        return generatorService.GenerateFromSchema(generator.node);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenerator(int id, [FromBody] JsonSchemaNode node)
    {
        throw new NotImplementedException();
    }
}
