using MediatR;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Enums;
using TitanFitness.Domain.Interfaces;
using CheckInEntity = TitanFitness.Domain.Entities.CheckIn;

namespace TitanFitness.Application.Features.CheckIn.Commands.CreateCheckIn;

public class CreateCheckInCommandHandler
    : IRequestHandler<CreateCheckInCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCheckInCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreateCheckInCommand request,
        CancellationToken cancellationToken)
    {
        var member = await _unitOfWork
            .Members
            .GetByIdAsync(request.MemberId);

        if (member is null)
        {
            throw new KeyNotFoundException("Member not found.");
        }

        var branch = await _unitOfWork
            .Branches
            .GetByIdAsync(request.BranchId);

        if (branch is null)
        {
            throw new KeyNotFoundException("Branch not found.");
        }

        var memberships = await _unitOfWork
            .Repository<Membership>()
            .FindAsync(m => m.MemberId == request.MemberId);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var membership = memberships
            .OrderByDescending(m => m.StartDate)
            .FirstOrDefault(m =>
                m.StartDate <= today &&
                m.EndDate >= today &&
                m.Status != MembershipStatus.Cancelled &&
                m.Status != MembershipStatus.Expired);

        CheckInResult result;
        string? refusalReason = null;

        if (membership is null)
        {
            result = CheckInResult.Refused;
            refusalReason = "No active membership.";
        }
        else if (membership.Status == MembershipStatus.Frozen)
        {
            result = CheckInResult.Refused;
            refusalReason = "Membership is frozen.";
        }
        else if (
            membership.AgreedTerms.AccessScope == AccessScope.HomeBranchOnly &&
            member.HomeBranchId != request.BranchId)
        {
            result = CheckInResult.Refused;
            refusalReason = "Membership does not allow access to this branch.";
        }
        else
        {
            result = CheckInResult.Admitted;
        }

        var checkIn = new CheckInEntity(
             request.MemberId,
            request.BranchId,
            DateTime.UtcNow,
            result,
            refusalReason);

        await _unitOfWork
            .Repository<CheckInEntity>()
            .AddAsync(checkIn);

        await _unitOfWork.SaveChangesAsync();

        return checkIn.CheckInId;
    }
}
