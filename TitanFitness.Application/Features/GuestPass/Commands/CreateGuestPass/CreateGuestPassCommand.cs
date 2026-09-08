using MediatR;

namespace TitanFitness.Application.Features.GuestPass.Commands.CreateGuestPass;

public record CreateGuestPassCommand(
    int MembershipId,
    DateOnly IssuedOn,
    string? GuestName
) : IRequest<int>;
