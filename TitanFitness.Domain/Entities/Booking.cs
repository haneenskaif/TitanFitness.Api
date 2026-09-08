using TitanFitness.Domain.Enums;

namespace TitanFitness.Domain.Entities;

public class Booking
{
    public int BookingId { get; private set; }

    public int SessionId { get; private set; }

    public int MemberId { get; private set; }

    public DateTime BookedOn { get; private set; }

    public BookingStatus Status { get; private set; }

    public int? WaitlistPosition { get; private set; }

    public string? Notes { get; private set; }

    private Booking()
    {
    }

    public Booking(
        int sessionId,
        int memberId,
        DateTime bookedOn,
        BookingStatus status,
        int? waitlistPosition,
        string? notes)
    {
        SessionId = sessionId;
        MemberId = memberId;
        BookedOn = bookedOn;
        Status = status;
        WaitlistPosition = waitlistPosition;
        Notes = notes;
    }
    public void Cancel()
    {
        if (Status == BookingStatus.Cancelled)
            throw new InvalidOperationException(
                "Booking is already cancelled.");

        Status = BookingStatus.Cancelled;
    }
    public void PromoteFromWaitlist()
    {
        if (Status != BookingStatus.Waitlisted)
            throw new InvalidOperationException(
                "Only a waitlisted booking can be promoted.");

        Status = BookingStatus.Booked;
        WaitlistPosition = null;
    }
}