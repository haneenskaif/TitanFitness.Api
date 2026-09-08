using MediatR;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Enums;
using TitanFitness.Domain.Interfaces;

using FreezeEntity = TitanFitness.Domain.Entities.Freeze;

namespace TitanFitness.Application.Features.Freeze.Commands.CreateFreeze;

public class CreateFreezeCommandHandler
    : IRequestHandler<CreateFreezeCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateFreezeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreateFreezeCommand request,
        CancellationToken cancellationToken)
    {
        var membership = await _unitOfWork
            .Repository<Membership>()
            .GetByIdAsync(request.MembershipId);

        if (membership is null)
        {
            throw new KeyNotFoundException(
                "Membership not found.");
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (request.StartDate < today)
        {
            throw new InvalidOperationException(
                "Freeze start date cannot be in the past.");
        }

        if (membership.Status is MembershipStatus.Cancelled
            or MembershipStatus.Expired)
        {
            throw new InvalidOperationException(
                "This membership cannot be frozen.");
        }

        if (membership.StartDate > request.StartDate)
        {
            throw new InvalidOperationException(
                "Membership has not started yet.");
        }

        if (request.DurationInMonths is < 1 or > 3)
        {
            throw new InvalidOperationException(
                "Freeze duration must be 1, 2, or 3 months.");
        }

        var existingFreezes = await _unitOfWork
            .Repository<FreezeEntity>()
            .FindAsync(
                f => f.MembershipId == request.MembershipId);

        var freezeCount = existingFreezes.Count();

        if (freezeCount >= membership.AgreedTerms.MaximumFreezes)
        {
            throw new InvalidOperationException(
                "Maximum number of freezes has been reached.");
        }

        var projectedEndDate = request.StartDate
            .AddMonths(request.DurationInMonths)
            .AddDays(-1);

        if (projectedEndDate > membership.EndDate)
        {
            throw new InvalidOperationException(
                "Freeze cannot extend beyond membership end date.");
        }

        var freezeDays =
            projectedEndDate.DayNumber
            - request.StartDate.DayNumber
            + 1;

        var usedFreezeDays = existingFreezes.Sum(
            f => f.EndDate.DayNumber
               - f.StartDate.DayNumber
               + 1);

        if (usedFreezeDays + freezeDays >
            membership.AgreedTerms.MaximumFreezeDays)
        {
            throw new InvalidOperationException(
                "Maximum freeze days allowance has been exceeded.");
        }

        var freeze = new FreezeEntity(
            membership.MembershipId,
            request.StartDate,
            projectedEndDate,
            request.DurationInMonths,
            request.Reason,
            request.AdditionalNotes,
            DateTime.UtcNow
        );

        membership.ApplyFreeze(
            request.StartDate,
            projectedEndDate);

        await _unitOfWork
            .Repository<FreezeEntity>()
            .AddAsync(freeze);

        await _unitOfWork.SaveChangesAsync();

        return freeze.FreezeId;
    }
}