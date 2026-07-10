using Microsoft.AspNetCore.Mvc;

namespace MsItem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger Logger;

    protected BaseController(ILoggerFactory loggerFactory)
        => Logger = loggerFactory.CreateLogger(GetType());
}
