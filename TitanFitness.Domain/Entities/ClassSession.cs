using TitanFitness.Domain.Enums;

namespace TitanFitness.Domain.Entities;

public class ClassSession
{
    public int SessionId { get; private set; }

    public string ClassName { get; private set; } = null!;

    public int BranchId { get; private set; }

    public int StudioId { get; private set; }

    public int TrainerId { get; private set; }

    public DateOnly SessionDate { get; private set; }

    public TimeOnly StartTime { get; private set; }

    public int DurationInMinutes { get; private set; }

    public int CapacityLimit { get; private set; }

    public ClassSessionStatus Status { get; private set; }

    public string? Description { get; private set; }

    private ClassSession()
    {
    }

    public ClassSession(
        string className,
        int branchId,
        int studioId,
        int trainerId,
        DateOnly sessionDate,
        TimeOnly startTime,
        int durationInMinutes,
        int capacityLimit,
        ClassSessionStatus status,
        string? description)
    {
        ClassName = className;
        BranchId = branchId;
        StudioId = studioId;
        TrainerId = trainerId;
        SessionDate = sessionDate;
        StartTime = startTime;
        DurationInMinutes = durationInMinutes;
        CapacityLimit = capacityLimit;
        Status = status;
        Description = description;
    }
}
