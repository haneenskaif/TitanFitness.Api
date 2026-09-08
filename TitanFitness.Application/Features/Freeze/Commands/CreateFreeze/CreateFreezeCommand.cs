using MediatR;
using TitanFitness.Domain.Enums;

namespace TitanFitness.Application.Features.Freeze.Commands.CreateFreeze;

public record CreateFreezeCommand(
    int MembershipId,
    DateOnly StartDate,
    int DurationInMonths,
    FreezeReason Reason,
    string? AdditionalNotes
) : IRequest<int>;
