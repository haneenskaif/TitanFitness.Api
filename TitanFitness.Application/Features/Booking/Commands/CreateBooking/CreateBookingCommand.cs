using MediatR;

namespace TitanFitness.Application.Features.Booking.Commands.CreateBooking;

public record CreateBookingCommand(
    int SessionId,
    int MemberId,
    string? Notes
) : IRequest<int>;
