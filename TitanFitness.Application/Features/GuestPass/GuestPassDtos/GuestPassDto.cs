using TitanFitness.Domain.Enums;

namespace TitanFitness.Application.Features.GuestPass.GuestPassDtos;

public class GuestPassDto
{
    public int GuestPassId { get; set; }

    public int MembershipId { get; set; }

    public DateOnly IssuedOn { get; set; }

    public DateOnly? UsedOn { get; set; }

    public string? GuestName { get; set; }
}
