using MediatR;
using TitanFitness.Domain.Enums;

namespace TitanFitness.Application.Features.Plans.Commands.CreatePlan;

public record CreatePlanCommand(
    string PlanName,
    decimal Price,
    int DurationInMonths,
    int MaximumFreezeDays,
    int MaximumFreezes,
    int GuestPassQuota,
    AccessScope AccessScope,
    bool IsPublished
) : IRequest<int>;
