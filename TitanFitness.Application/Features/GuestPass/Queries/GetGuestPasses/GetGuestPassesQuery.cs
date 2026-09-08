using MediatR;
using TitanFitness.Application.Features.GuestPass.GuestPassDtos;

namespace TitanFitness.Application.Features.GuestPass.Queries.GetGuestPasses;

public record GetGuestPassesQuery(
    int MembershipId
) : IRequest<IEnumerable<GuestPassDto>>;