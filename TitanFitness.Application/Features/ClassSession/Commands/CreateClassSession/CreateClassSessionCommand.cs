using MediatR;
using TitanFitness.Domain.Enums;

namespace TitanFitness.Application.Features.ClassSession.Commands.CreateClassSession;

public record CreateClassSessionCommand(
    string ClassName,
    int BranchId,
    int StudioId,
    int TrainerId,
    DateOnly SessionDate,
    TimeOnly StartTime,
    int DurationInMinutes,
    int CapacityLimit,
    ClassSessionStatus Status,
    string? Description
) : IRequest<int>;
