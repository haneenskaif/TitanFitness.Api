using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.CheckIn.Commands.CreateCheckIn;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckInController : ControllerBase
{
    private readonly IMediator _mediator;

    public CheckInController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(
        CreateCheckInCommand command)
    {
        var checkInId = await _mediator.Send(command);

        return Ok(new { checkInId });
    }
}
