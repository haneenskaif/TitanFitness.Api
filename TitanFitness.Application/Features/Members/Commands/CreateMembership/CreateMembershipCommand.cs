using MediatR;
using TitanFitness.Application.Features.Memberships.DTOs;

namespace TitanFitness.Application.Features.Memberships.Commands.CreateMembership;

public record CreateMembershipCommand(
    int MemberId,
    int PlanId,
    DateOnly StartDate
) : IRequest<MembershipDto>;
