using MediatR;
using TitanFitness.Application.Features.Memberships.DTOs;

namespace TitanFitness.Application.Features.Memberships.Queries.GetMemberships;

public record GetMembershipsQuery : IRequest<IEnumerable<MembershipDto>>;
