using MediatR;
using TitanFitness.Application.Features.Branches.DTOs;

namespace TitanFitness.Application.Features.Branches.Commands.CreateBranch;

public record CreateBranchCommand(
    string BranchName,
    string Address,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime
) : IRequest<BranchDto>;
