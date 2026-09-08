namespace TitanFitness.Domain.ValueObjects;

public sealed class MembershipNumber
{
    public string Value { get; private set; }

    private MembershipNumber()
    {
        Value = null!;
    }

    public MembershipNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "Membership number is required.",
                nameof(value));

        if (value.Length > 10)
            throw new ArgumentException(
                "Membership number cannot exceed 10 characters.",
                nameof(value));

        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}
