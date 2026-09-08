using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.Branches.Commands.CreateBranch;
using TitanFitness.Application.Features.Branches.DTOs;
using TitanFitness.Application.Features.Branches.Queries.GetBranches;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<BranchDto>> Create(
        CreateBranchCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BranchDto>>> GetAll()
    {
        var query = new GetBranchesQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
