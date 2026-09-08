using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.GuestPass.Commands.CreateGuestPass;
using TitanFitness.Application.Features.GuestPass.Queries.GetGuestPasses;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestPassController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuestPassController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(
        CreateGuestPassCommand command)
    {
        var guestPassId = await _mediator.Send(command);

        return Ok(new { guestPassId });
    }

    [HttpGet("{membershipId:int}")]
    public async Task<ActionResult> GetByMembership(
        int membershipId)
    {
        var result = await _mediator.Send(
            new GetGuestPassesQuery(membershipId));

        return Ok(result);
    }
}
