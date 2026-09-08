using TitanFitness.Domain.ValueObjects;

namespace TitanFitness.Domain.Entities;

public class Member
{
    public int MemberId { get; private set; }

    public MembershipNumber MembershipNumber { get; private set; } = null!;

    public string FullName { get; private set; } = null!;

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public string? Address { get; private set; }

    public DateOnly JoinedDate { get; private set; }

    public string? Photo { get; private set; }

    public int HomeBranchId { get; private set; }

    private Member()
    {
    }

    public Member(
        MembershipNumber membershipNumber,
        string fullName,
        string? email,
        string? phone,
        string? address,
        DateOnly joinedDate,
        string? photo,
        int homeBranchId)
    {
        MembershipNumber = membershipNumber;
        FullName = fullName;
        Email = email;
        Phone = phone;
        Address = address;
        JoinedDate = joinedDate;
        Photo = photo;
        HomeBranchId = homeBranchId;
    }
}
