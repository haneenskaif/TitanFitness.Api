using TitanFitness.Domain.Enums;

namespace TitanFitness.Domain.ValueObjects;

public sealed class AgreedTerms
{
    public decimal PricePaid { get; private set; }

    public int DurationInMonths { get; private set; }

    public int MaximumFreezeDays { get; private set; }

    public int MaximumFreezes { get; private set; }

    public int GuestPassQuota { get; private set; }

    public AccessScope AccessScope { get; private set; }

    private AgreedTerms()
    {
    }

    public AgreedTerms(
        decimal pricePaid,
        int durationInMonths,
        int maximumFreezeDays,
        int maximumFreezes,
        int guestPassQuota,
        AccessScope accessScope)
    {
        PricePaid = pricePaid;
        DurationInMonths = durationInMonths;
        MaximumFreezeDays = maximumFreezeDays;
        MaximumFreezes = maximumFreezes;
        GuestPassQuota = guestPassQuota;
        AccessScope = accessScope;
    }
}
