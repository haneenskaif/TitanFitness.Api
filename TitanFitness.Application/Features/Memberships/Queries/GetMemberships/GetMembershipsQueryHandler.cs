using MediatR;
using TitanFitness.Application.Features.Memberships.DTOs;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;

namespace TitanFitness.Application.Features.Memberships.Queries.GetMemberships;

public class GetMembershipsQueryHandler
    : IRequestHandler<GetMembershipsQuery, IEnumerable<MembershipDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMembershipsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MembershipDto>> Handle(
        GetMembershipsQuery request,
        CancellationToken cancellationToken)
    {
        var memberships = await _unitOfWork
            .Repository<Membership>()
            .GetAllAsync();

        return memberships.Select(membership => new MembershipDto
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
        });
    }
}
