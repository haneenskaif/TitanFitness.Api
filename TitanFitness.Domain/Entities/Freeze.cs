using TitanFitness.Domain.Enums;

namespace TitanFitness.Domain.Entities;

public class Freeze
{
    public int FreezeId { get; private set; }

    public int MembershipId { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public int DurationInMonths { get; private set; }

    public FreezeReason Reason { get; private set; }

    public string? AdditionalNotes { get; private set; }

    public DateTime RequestedOn { get; private set; }

    private Freeze()
    {
    }

    public Freeze(
        int membershipId,
        DateOnly startDate,
        DateOnly endDate,
        int durationInMonths,
        FreezeReason reason,
        string? additionalNotes,
        DateTime requestedOn)
    {
        MembershipId = membershipId;
        StartDate = startDate;
        EndDate = endDate;
        DurationInMonths = durationInMonths;
        Reason = reason;
        AdditionalNotes = additionalNotes;
        RequestedOn = requestedOn;
    }
}