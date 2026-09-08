namespace TitanFitness.Domain.Entities;

public class Studio
{
    public int StudioId { get; private set; }

    public string StudioName { get; private set; } = null!;

    public int BranchId { get; private set; }

    public int Capacity { get; private set; }

    private Studio()
    {
    }

    public Studio(
        string studioName,
        int branchId,
        int capacity)
    {
        StudioName = studioName;
        BranchId = branchId;
        Capacity = capacity;
    }
}
