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
public class GeneratorsContoller(IGeneratorService generatorService, IGeneratorRepository generatorRepository)
{
    [HttpPost]
    public ActionResult<int> CreateGenerator([FromBody] JsonSchemaNode node)
    {
        return generatorRepository.Add(node);
    }

    [HttpGet("{id}/generate")]
    public ActionResult<ExpandoObject> GenerateJson(int id)
    {
        var generator = generatorRepository.Get(id);
        return generatorService.GenerateFromSchema(generator.node);
    }
}
