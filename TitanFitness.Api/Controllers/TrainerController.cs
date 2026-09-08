using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.Trainer.Commands.CreateTrainer;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainerController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrainerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(
        CreateTrainerCommand command)
    {
        var trainerId = await _mediator.Send(command);

        return Ok(new { trainerId });
    }
}
