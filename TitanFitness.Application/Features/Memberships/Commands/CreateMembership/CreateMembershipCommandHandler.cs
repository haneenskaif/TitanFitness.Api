using MediatR;
using TitanFitness.Application.Features.Memberships.DTOs;
using TitanFitness.Application.Features.Memberships.DTOs;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Enums;
using TitanFitness.Domain.Interfaces;
using TitanFitness.Domain.ValueObjects;
using TitanFitness.Application.Features.Memberships.DTOs;

namespace TitanFitness.Application.Features.Memberships.Commands.CreateMembership;

public class CreateMembershipCommandHandler
    : IRequestHandler<CreateMembershipCommand, MembershipDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMembershipCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MembershipDto> Handle(
         CreateMembershipCommand request,
        CancellationToken cancellationToken)
    {
        var member = await _unitOfWork
            .Repository<Member>()
            .GetByIdAsync(request.MemberId);

        if (member is null)
        {
            throw new KeyNotFoundException(
                "Member not found.");
        }

        var plan = await _unitOfWork
            .Repository<Plan>()
            .GetByIdAsync(request.PlanId);

        if (plan is null)
        {
            throw new KeyNotFoundException(
                "Plan not found.");
        }

        if (!plan.IsPublished)
        {
            throw new InvalidOperationException(
                "This plan is not published and cannot be sold.");
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (request.StartDate < today)
        {
            throw new InvalidOperationException(
                "Membership start date cannot be in the past.");
        }

        var agreedTerms = new AgreedTerms(
            plan.Price,
            plan.DurationInMonths,
            plan.MaximumFreezeDays,
            plan.MaximumFreezes,
            plan.GuestPassQuota,
            plan.AccessScope
        );

        var endDate = request.StartDate.AddMonths(
            plan.DurationInMonths);

        var status = request.StartDate > today
            ? MembershipStatus.Pending
            : MembershipStatus.Active;

        var membership = new Membership(
            request.MemberId,
            request.PlanId,
            DateTime.UtcNow,
            request.StartDate,
            endDate,
            status,
            agreedTerms
        );

        await _unitOfWork
            .Repository<Membership>()
            .AddAsync(membership);

        await _unitOfWork.SaveChangesAsync();

        return new MembershipDto
        {
            MembershipId = membership.MembershipId,
            MemberId = membership.MemberId,
            PlanId = membership.PlanId,
            PurchaseDate = membership.PurchaseDate,
            StartDate = membership.StartDate,
            EndDate = membership.EndDate,
            Status = membership.Status,
            PricePaid = membership.AgreedTerms.PricePaid,
            DurationInMonths = membership.AgreedTerms.DurationInMonths,
            MaximumFreezeDays = membership.AgreedTerms.MaximumFreezeDays,
            MaximumFreezes = membership.AgreedTerms.MaximumFreezes,
            GuestPassQuota = membership.AgreedTerms.GuestPassQuota,
            AccessScope = membership.AgreedTerms.AccessScope
        };
    }
}
