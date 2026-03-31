using System.Dynamic;
using kontur_hack2026.Models;
using kontur_hack2026.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace kontur_hack2026.Controllers;

[Controller]
[Route("generators")]
public class GeneratorsContoller(IGeneratorService generatorService)
{
    [HttpPost]
    public ActionResult<ExpandoObject> GenerateJson([FromBody] JsonSchemaNode node)
    {
        return generatorService.GenerateFromSchema(node);
    }
}