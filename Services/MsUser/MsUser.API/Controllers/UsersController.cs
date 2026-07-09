using Mediator;
using Microsoft.AspNetCore.Mvc;
using MsUser.Application.Features.Users.Commands.CreateUser;
using MsUser.Application.Features.Users.Commands.UpdateUserWorkload;
using MsUser.Application.Features.Users.Queries.GetAllUsers;
using MsUser.Application.Features.Users.Queries.GetAvailableUsers;
using MsUser.Application.Features.Users.Queries.GetUserByUsername;

namespace MsUser.API.Controllers;

public class UsersController(
    ILoggerFactory loggerFactory,
    IMediator mediator) : BaseController(loggerFactory)
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        Logger.LogInformation("{Method}: username={Username}", nameof(Create), command.Username);

        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByUsername), new { username = result.Username }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Logger.LogInformation("{Method}", nameof(GetAll));

        var result = await mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        Logger.LogInformation("{Method}: username={Username}", nameof(GetByUsername), username);

        var result = await mediator.Send(new GetUserByUsernameQuery(username));
        return Ok(result);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        Logger.LogInformation("{Method}", nameof(GetAvailable));

        var result = await mediator.Send(new GetAvailableUsersQuery());
        return Ok(result);
    }

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