using MediatR;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;

namespace TitanFitness.Application.Features.Plans.Commands.CreatePlan;

public class CreatePlanCommandHandler
    : IRequestHandler<CreatePlanCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreatePlanCommand request,
        CancellationToken cancellationToken)
    {
        var plan = new Plan(
            request.PlanName,
            request.Price,
            request.DurationInMonths,
            request.MaximumFreezeDays,
            request.MaximumFreezes,
            request.GuestPassQuota,
            request.AccessScope,
            request.IsPublished
        );

        await _unitOfWork.Plans.AddAsync(plan);

        await _unitOfWork.SaveChangesAsync();

        return plan.PlanId;
    }
}
