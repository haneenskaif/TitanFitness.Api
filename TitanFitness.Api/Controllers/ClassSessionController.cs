using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.ClassSession.Commands.CreateClassSession;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassSessionController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassSessionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(
        CreateClassSessionCommand command)
    {
        var sessionId = await _mediator.Send(command);

        return Ok(new { sessionId });
    }
}
