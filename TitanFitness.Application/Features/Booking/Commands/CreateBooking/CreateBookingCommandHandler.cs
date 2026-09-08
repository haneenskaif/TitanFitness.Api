using MediatR;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Enums;
using TitanFitness.Domain.Interfaces;

using BookingEntity = TitanFitness.Domain.Entities.Booking;
using ClassSessionEntity = TitanFitness.Domain.Entities.ClassSession;

namespace TitanFitness.Application.Features.Booking.Commands.CreateBooking;

public class CreateBookingCommandHandler
    : IRequestHandler<CreateBookingCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
        var member = await _unitOfWork
            .Members
            .GetByIdAsync(request.MemberId);

        if (member is null)
            throw new KeyNotFoundException("Member not found.");

        var session = await _unitOfWork
            .Repository<ClassSessionEntity>()
            .GetByIdAsync(request.SessionId);

        if (session is null)
            throw new KeyNotFoundException("Class session not found.");

        if (session.Status is
            ClassSessionStatus.InProgress or
            ClassSessionStatus.Completed or
            ClassSessionStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "This session does not accept bookings.");
        }

        var memberships = await _unitOfWork
            .Repository<Membership>()
            .FindAsync(m => m.MemberId == request.MemberId);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var membership = memberships
            .FirstOrDefault(m =>
                m.Status == MembershipStatus.Active &&
                m.StartDate <= today &&
                m.EndDate >= today);

        if (membership is null)
        {
            throw new InvalidOperationException(
                "Member must have an active membership.");
        }

        var existingBookings = await _unitOfWork
            .Repository<BookingEntity>()
            .FindAsync(b =>
                b.SessionId == request.SessionId &&
                b.MemberId == request.MemberId);

        if (existingBookings.Any(b =>
            b.Status != BookingStatus.Cancelled))
        {
            throw new InvalidOperationException(
                "Member is already booked or waitlisted for this session.");
        }

        var sessionStart = session.SessionDate
            .ToDateTime(session.StartTime);

        var sessionEnd = sessionStart
            .AddMinutes(session.DurationInMinutes);

        if (DateTime.UtcNow >= sessionStart)
        {
            throw new InvalidOperationException(
                "Session has already started.");
        }

        var memberBookings = await _unitOfWork
            .Repository<BookingEntity>()
            .FindAsync(b =>
                b.MemberId == request.MemberId &&
                b.Status == BookingStatus.Booked);

        var sessions = await _unitOfWork
            .Repository<ClassSessionEntity>()
            .GetAllAsync();

        foreach (var booking in memberBookings)
        {
            var otherSession = sessions
                .FirstOrDefault(s => s.SessionId == booking.SessionId);

            if (otherSession is null)
                continue;

            var otherStart = otherSession.SessionDate
                .ToDateTime(otherSession.StartTime);

            var otherEnd = otherStart
                .AddMinutes(otherSession.DurationInMinutes);

            if (sessionStart < otherEnd &&
                otherStart < sessionEnd)
            {
                throw new InvalidOperationException(
                    "Member already has an overlapping session.");
            }
        }

        var sessionBookings = await _unitOfWork
            .Repository<BookingEntity>()
            .FindAsync(b => b.SessionId == request.SessionId);

        var bookedCount = sessionBookings.Count(b =>
            b.Status == BookingStatus.Booked ||
            b.Status == BookingStatus.Attended);

        BookingStatus status;
        int? waitlistPosition = null;

        if (bookedCount < session.CapacityLimit)
        {
            status = BookingStatus.Booked;
        }
        else
        {
            status = BookingStatus.Waitlisted;

            waitlistPosition = sessionBookings
                .Count(b => b.Status == BookingStatus.Waitlisted) + 1;
        }

        var bookingEntity = new BookingEntity(
            request.SessionId,
            request.MemberId,
            DateTime.UtcNow,
            status,
            waitlistPosition,
            request.Notes);

        await _unitOfWork
            .Repository<BookingEntity>()
            .AddAsync(bookingEntity);

        await _unitOfWork.SaveChangesAsync();

        return bookingEntity.BookingId;
    }
}
