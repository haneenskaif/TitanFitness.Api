using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.Memberships.Commands.CreateMembership;
using TitanFitness.Application.Features.Memberships.DTOs;
using TitanFitness.Application.Features.Memberships.Queries.GetMemberships;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembershipsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembershipsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<MembershipDto>> Create(
        CreateMembershipCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembershipDto>>> GetAll()
    {
        var query = new GetMembershipsQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}