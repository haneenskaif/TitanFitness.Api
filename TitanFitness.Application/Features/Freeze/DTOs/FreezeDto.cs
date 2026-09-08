using TitanFitness.Domain.Enums;

namespace TitanFitness.Application.Features.Freeze.DTOs;

public class FreezeDto
{
    public int FreezeId { get; set; }

    public int MembershipId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int DurationInMonths { get; set; }

    public FreezeReason Reason { get; set; }

    public string? AdditionalNotes { get; set; }

    public DateTime RequestedOn { get; set; }
}