namespace TitanFitness.Domain.Entities;

public class Branch
{
    public int BranchId { get; private set; }

    public string BranchName { get; private set; } = null!;

    public string? Address { get; private set; }

    public TimeOnly OpeningTime { get; private set; }

    public TimeOnly ClosingTime { get; private set; }

    private Branch()
    {
    }

    public Branch(
        string branchName,
        string? address,
        TimeOnly openingTime,
        TimeOnly closingTime)
    {
        BranchName = branchName;
        Address = address;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
    }
}
