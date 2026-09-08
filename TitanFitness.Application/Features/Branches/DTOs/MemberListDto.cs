namespace TitanFitness.Application.Features.Members.DTOs;

public class MemberListDto
{
    public int MemberId { get; set; }

    public string MembershipNumber { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string BranchName { get; set; } = string.Empty;

    public DateTime? LastVisit { get; set; }
}
