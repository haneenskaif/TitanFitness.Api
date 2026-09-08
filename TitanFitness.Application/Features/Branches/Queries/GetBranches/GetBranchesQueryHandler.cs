using MediatR;
using TitanFitness.Application.Features.Branches.DTOs;
using TitanFitness.Domain.Interfaces;

namespace TitanFitness.Application.Features.Branches.Queries.GetBranches;

public class GetBranchesQueryHandler
    : IRequestHandler<GetBranchesQuery, IEnumerable<BranchDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBranchesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BranchDto>> Handle(
        GetBranchesQuery request,
        CancellationToken cancellationToken)
    {
        var branches = await _unitOfWork.Branches.GetAllAsync();

        return branches.Select(branch => new BranchDto
        {
            BranchId = branch.BranchId,
            BranchName = branch.BranchName,
            Address = branch.Address,
            OpeningTime = branch.OpeningTime,
            ClosingTime = branch.ClosingTime
        });
    }
}
