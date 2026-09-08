using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.Freeze.Commands.CreateFreeze;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FreezeController : ControllerBase
{
    private readonly IMediator _mediator;

    public FreezeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(
        CreateFreezeCommand command)
    {
        var freezeId = await _mediator.Send(command);

        return Ok(new
        {
            freezeId
        });
    }
}
