using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.Members.Commands.CreateMember;
using TitanFitness.Application.Features.Members.DTOs;
using TitanFitness.Application.Features.Members.Queries.GetMembers;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> Create(
        CreateMemberCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<PagedResult<MemberListDto>>> GetAll(
        [FromQuery] string? search = null,
        [FromQuery] int? branchId = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetMembersQuery(
            search,
            branchId,
            pageNumber,
            pageSize);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}