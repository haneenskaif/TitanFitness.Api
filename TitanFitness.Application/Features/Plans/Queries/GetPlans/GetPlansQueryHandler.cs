using MediatR;
using TitanFitness.Application.Features.Plans.DTOs;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;

namespace TitanFitness.Application.Features.Plans.Queries.GetPlans;

public class GetPlansQueryHandler
    : IRequestHandler<GetPlansQuery, IEnumerable<PlanDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPlansQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PlanDto>> Handle(
        GetPlansQuery request,
        CancellationToken cancellationToken)
    {
        var plans = await _unitOfWork
            .Repository<Plan>()
            .GetAllAsync();

        return plans.Select(plan => new PlanDto
        {
            PlanId = plan.PlanId,
            PlanName = plan.PlanName,
            Price = plan.Price,
            DurationInMonths = plan.DurationInMonths,
            MaximumFreezeDays = plan.MaximumFreezeDays,
            MaximumFreezes = plan.MaximumFreezes,
            GuestPassQuota = plan.GuestPassQuota,
            AccessScope = plan.AccessScope,
            IsPublished = plan.IsPublished
        });
    }
}
