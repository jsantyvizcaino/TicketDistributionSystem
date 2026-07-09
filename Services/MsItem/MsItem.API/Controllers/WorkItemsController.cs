using Mediator;
using Microsoft.AspNetCore.Mvc;
using MsItem.Application.Features.WorkItems.Commands.AssignWorkItem;
using MsItem.Application.Features.WorkItems.Commands.CompleteWorkItem;
using MsItem.Application.Features.WorkItems.Commands.CreateWorkItem;
using MsItem.Application.Features.WorkItems.Commands.UpdateWorkItem;
using MsItem.Application.Features.WorkItems.Queries.GetAllWorkItems;
using MsItem.Application.Features.WorkItems.Queries.GetPendingWorkItems;
using MsItem.Application.Features.WorkItems.Queries.GetWorkItemById;
using MsItem.Application.Features.WorkItems.Queries.GetWorkItemsByUsername;

namespace MsItem.API.Controllers;

public class WorkItemsController(
    ILoggerFactory loggerFactory,
    IMediator mediator) : BaseController(loggerFactory)
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWorkItemCommand command)
    {
        Logger.LogInformation("{Method}: title={Title}", nameof(Create), command.Title);

        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Logger.LogInformation("{Method}", nameof(GetAll));

        var result = await mediator.Send(new GetAllWorkItemsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Logger.LogInformation("{Method}: id={Id}", nameof(GetById), id);

        var result = await mediator.Send(new GetWorkItemByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        Logger.LogInformation("{Method}", nameof(GetPending));

        var result = await mediator.Send(new GetPendingWorkItemsQuery());
        return Ok(result);
    }

    [HttpGet("user/{username}")]
    public async Task<IActionResult> GetByUsername(string username)
    {
        Logger.LogInformation("{Method}: username={Username}", nameof(GetByUsername), username);

        var result = await mediator.Send(new GetWorkItemsByUsernameQuery(username));
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkItemCommand command)
    {
        Logger.LogInformation("{Method}: id={Id}", nameof(Update), id);

        var updated = command with { WorkItemId = id };
        var result = await mediator.Send(updated);
        return Ok(result);
    }

    [HttpPut("{id:guid}/assign")]
    public async Task<IActionResult> Assign(Guid id, [FromBody] AssignWorkItemCommand command)
    {
        Logger.LogInformation("{Method}: id={Id}", nameof(Assign), id);

        var updated = command with { WorkItemId = id };
        var result = await mediator.Send(updated);
        return Ok(result);
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        Logger.LogInformation("{Method}: id={Id}", nameof(Complete), id);

        var result = await mediator.Send(new CompleteWorkItemCommand(id));
        return Ok(result);
    }
}
