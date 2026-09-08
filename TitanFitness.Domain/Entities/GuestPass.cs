namespace TitanFitness.Domain.Entities;

public class GuestPass
{
    public int GuestPassId { get; private set; }

    public int MembershipId { get; private set; }

    public DateOnly IssuedOn { get; private set; }

    public DateOnly? UsedOn { get; private set; }

    public string? GuestName { get; private set; }

    private GuestPass()
    {
    }

    public GuestPass(
        int membershipId,
        DateOnly issuedOn,
        string? guestName)
    {
        MembershipId = membershipId;
        IssuedOn = issuedOn;
        GuestName = guestName;
    }

    public void Use(DateOnly usedOn, string guestName)
    {
        if (UsedOn.HasValue)
            throw new InvalidOperationException(
                "Guest pass has already been used.");

        UsedOn = usedOn;
        GuestName = guestName;
    }
}