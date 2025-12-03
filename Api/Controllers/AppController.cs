using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("")]
public class AppController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> HelloWorld()
    {
        return Ok("Hello world from a .NET API!");
    }
}