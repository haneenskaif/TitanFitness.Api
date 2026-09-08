namespace TitanFitness.Application.Features.Branches.DTOs;

public class BranchDto
{
    public int BranchId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public TimeOnly OpeningTime { get; set; }

    public TimeOnly ClosingTime { get; set; }
}
