using MediatR;
using TitanFitness.Application.Features.Plans.DTOs;

namespace TitanFitness.Application.Features.Plans.Queries.GetPlans;

public record GetPlansQuery : IRequest<IEnumerable<PlanDto>>;