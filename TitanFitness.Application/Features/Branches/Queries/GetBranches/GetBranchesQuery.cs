using MediatR;
using TitanFitness.Application.Features.Branches.DTOs;

namespace TitanFitness.Application.Features.Branches.Queries.GetBranches;

public record GetBranchesQuery : IRequest<IEnumerable<BranchDto>>;
