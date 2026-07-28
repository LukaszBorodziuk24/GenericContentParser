using GenericContentParser.Models;
using GenericContentParser.Services;
using Microsoft.AspNetCore.Mvc;

namespace GenericContentParser.Controllers;

[ApiController]
[Route("api/v1")]
public class ParserController(ContentParserService parserService) : ControllerBase
{
    [HttpPost("parse-content")]
    [Consumes("application/json")]
    public IActionResult Parse(ParseRequest request)
    {
        var response = parserService.Parse(request);
        return Ok(response);
    }
}