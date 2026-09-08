using TitanFitness.Domain.Enums;
using TitanFitness.Domain.ValueObjects;

namespace TitanFitness.Domain.Entities;

public class Membership
{
    public int MembershipId { get; private set; }

    public int MemberId { get; private set; }

    public int PlanId { get; private set; }

    public DateTime PurchaseDate { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public MembershipStatus Status { get; private set; }

    public AgreedTerms AgreedTerms { get; private set; } = null!;

    private Membership()
    {
    }

    public Membership(
        int memberId,
        int planId,
        DateTime purchaseDate,
        DateOnly startDate,
        DateOnly endDate,
        MembershipStatus status,
        AgreedTerms agreedTerms)
    {
        MemberId = memberId;
        PlanId = planId;
        PurchaseDate = purchaseDate;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        AgreedTerms = agreedTerms;
    }

    public void Cancel()
    {
        if (Status == MembershipStatus.Cancelled)
            throw new InvalidOperationException(
                "Membership is already cancelled.");

        Status = MembershipStatus.Cancelled;
    }

    public void ApplyFreeze(
        DateOnly freezeStartDate,
        DateOnly freezeEndDate)
    {
        if (Status == MembershipStatus.Cancelled)
            throw new InvalidOperationException(
                "Cancelled membership cannot be frozen.");

        if (freezeStartDate < StartDate)
            throw new InvalidOperationException(
                "Freeze cannot start before membership starts.");

        if (freezeEndDate < freezeStartDate)
            throw new InvalidOperationException(
                "Freeze end date cannot be before freeze start date.");

        var freezeDays =
            freezeEndDate.DayNumber
            - freezeStartDate.DayNumber
            + 1;

        EndDate = EndDate.AddDays(freezeDays);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (freezeStartDate <= today && today <= freezeEndDate)
        {
            Status = MembershipStatus.Frozen;
        }
    }
}