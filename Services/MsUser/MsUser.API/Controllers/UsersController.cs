using Mediator;
using Microsoft.AspNetCore.Mvc;
using MsUser.Application.Features.Users.Commands.CreateUser;
using MsUser.Application.Features.Users.Commands.UpdateUserWorkload;
using MsUser.Application.Features.Users.Queries.GetAllUsers;
using MsUser.Application.Features.Users.Queries.GetAvailableUsers;
using MsUser.Application.Features.Users.Queries.GetUserByUsername;

namespace MsUser.API.Controllers;

/// <summary>
/// Expone las operaciones HTTP sobre usuarios: creación, consulta y actualización de carga de trabajo.
/// Delega toda la lógica de negocio en los Commands/Queries manejados vía Mediator.
/// </summary>
public class UsersController(
    ILoggerFactory loggerFactory,
    IMediator mediator) : BaseController(loggerFactory)
{
    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        Logger.LogInformation("{Method}: username={Username}", nameof(Create), command.Username);

        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByUsername), new { username = result.Username }, result);
    }

    /// <summary>
    /// Obtiene todos los usuarios registrados.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Logger.LogInformation("{Method}", nameof(GetAll));

        var result = await mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    /// <summary>
    /// Obtiene un usuario por su nombre de usuario.
    /// </summary>
    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        Logger.LogInformation("{Method}: username={Username}", nameof(GetByUsername), username);

        var result = await mediator.Send(new GetUserByUsernameQuery(username));
        return Ok(result);
    }

    /// <summary>
    /// Obtiene los usuarios disponibles (activos y no saturados) para el proceso de distribución.
    /// Es consumido por MsItem a través de <c>IUserApiClient</c>.
    /// </summary>
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        Logger.LogInformation("{Method}", nameof(GetAvailable));

        var result = await mediator.Send(new GetAvailableUsersQuery());
        return Ok(result);
    }

    /// <summary>
    /// Actualiza los contadores de carga de trabajo de un usuario.
    /// Es invocado por MsItem al asignar o completar un ítem de trabajo.
    /// </summary>
    [HttpPut("{username}/workload")]
    public async Task<IActionResult> UpdateWorkload(
        string username,
        [FromBody] UpdateUserWorkloadCommand command)
    {
        Logger.LogInformation("{Method}: username={Username}", nameof(UpdateWorkload), username);

        var updated = command with { Username = username };
        var result = await mediator.Send(updated);
        return Ok(result);
    }
}