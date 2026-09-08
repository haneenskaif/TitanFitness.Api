using MediatR;
using Microsoft.AspNetCore.Mvc;
using TitanFitness.Application.Features.Booking.Commands.CreateBooking;

namespace TitanFitness.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(
        CreateBookingCommand command)
    {
        var bookingId = await _mediator.Send(command);

        return Ok(new { bookingId });
    }
}
