using TitanFitness.Domain.Enums;

namespace TitanFitness.Domain.Entities;

public class Plan
{
    public int PlanId { get; private set; }

    public string PlanName { get; private set; } = null!;

    public decimal Price { get; private set; }

    public int DurationInMonths { get; private set; }

    public int MaximumFreezeDays { get; private set; }

    public int MaximumFreezes { get; private set; }

    public int GuestPassQuota { get; private set; }

    public AccessScope AccessScope { get; private set; }

    public bool IsPublished { get; private set; }

    private Plan()
    {
    }

    public Plan(
        string planName,
        decimal price,
        int durationInMonths,
        int maximumFreezeDays,
        int maximumFreezes,
        int guestPassQuota,
        AccessScope accessScope,
        bool isPublished)
    {
        PlanName = planName;
        Price = price;
        DurationInMonths = durationInMonths;
        MaximumFreezeDays = maximumFreezeDays;
        MaximumFreezes = maximumFreezes;
        GuestPassQuota = guestPassQuota;
        AccessScope = accessScope;
        IsPublished = isPublished;
    }
}
