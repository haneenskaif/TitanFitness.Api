using MediatR;
using TitanFitness.Application.Features.Branches.DTOs;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;

namespace TitanFitness.Application.Features.Branches.Commands.CreateBranch;

public class CreateBranchCommandHandler
    : IRequestHandler<CreateBranchCommand, BranchDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBranchCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto> Handle(
        CreateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var branch = new Branch(
            request.BranchName,
            request.Address,
            request.OpeningTime,
            request.ClosingTime
        );

        await _unitOfWork.Branches.AddAsync(branch);

        await _unitOfWork.SaveChangesAsync();

        return new BranchDto
        {
            BranchId = branch.BranchId,
            BranchName = branch.BranchName,
            Address = branch.Address,
            OpeningTime = branch.OpeningTime,
            ClosingTime = branch.ClosingTime
        };
    }
}
