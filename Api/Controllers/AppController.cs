using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("")]
public class AppController(IMessageService messageService) : ControllerBase
{
    [HttpGet]
    public ActionResult<string> HelloWorld()
    {
        return messageService.GetWelcomeMessage();
    }
}