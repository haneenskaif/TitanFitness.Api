using MediatR;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Enums;
using GuestPassEntity = TitanFitness.Domain.Entities.GuestPass;

namespace TitanFitness.Application.Features.GuestPass.Commands.CreateGuestPass;

public class CreateGuestPassCommandHandler
    : IRequestHandler<CreateGuestPassCommand, int>
{
    private readonly TitanFitness.Domain.Interfaces.IUnitOfWork _unitOfWork;

    public CreateGuestPassCommandHandler(
        TitanFitness.Domain.Interfaces.IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreateGuestPassCommand request,
        CancellationToken cancellationToken)
    {
        var membership = await _unitOfWork
            .Repository<Membership>()
            .GetByIdAsync(request.MembershipId);

        if (membership is null)
        {
            throw new KeyNotFoundException(
                "Membership not found.");
        }

        if (membership.Status is MembershipStatus.Cancelled
            or MembershipStatus.Expired)
        {
            throw new InvalidOperationException(
                "Guest pass cannot be issued for this membership.");
        }

        var existingGuestPasses = await _unitOfWork
           .Repository<GuestPassEntity>()
            .FindAsync(g =>
                g.MembershipId == request.MembershipId);

        var issuedGuestPassesCount = existingGuestPasses.Count();

        if (issuedGuestPassesCount >=
            membership.AgreedTerms.GuestPassQuota)
        {
            throw new InvalidOperationException(
                "Guest pass quota has been reached.");
        }

        var guestPass = new GuestPassEntity(
              membership.MembershipId,
            request.IssuedOn,
            request.GuestName
        );

        await _unitOfWork
            .Repository<GuestPassEntity>()
            .AddAsync(guestPass);

        await _unitOfWork.SaveChangesAsync();

        return guestPass.GuestPassId;
    }
}
