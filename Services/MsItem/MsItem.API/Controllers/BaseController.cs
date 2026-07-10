using Microsoft.AspNetCore.Mvc;

namespace MsItem.API.Controllers;

/// <summary>
/// Controlador base con configuración común de ruteo y acceso a logging para MsItem.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Logger específico del controlador derivado.
    /// </summary>
    protected readonly ILogger Logger;

    protected BaseController(ILoggerFactory loggerFactory)
        => Logger = loggerFactory.CreateLogger(GetType());
}
