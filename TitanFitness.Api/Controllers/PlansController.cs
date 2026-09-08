using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.Plans.Commands.CreatePlan;
using TitanFitness.Application.Features.Plans.DTOs;
using TitanFitness.Application.Features.Plans.Queries.GetPlans;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<PlanDto>> Create(
        CreatePlanCommand command)
    {
        var planId = await _mediator.Send(command);

        return Ok(new
        {
            planId
        });
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanDto>>> GetAll()
    {
        var query = new GetPlansQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
