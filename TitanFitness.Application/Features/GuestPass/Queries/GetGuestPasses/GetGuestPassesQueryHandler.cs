using MediatR;
using TitanFitness.Application.Features.GuestPass.GuestPassDtos;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;
using GuestPassEntity = TitanFitness.Domain.Entities.GuestPass;

namespace TitanFitness.Application.Features.GuestPass.Queries.GetGuestPasses;

public class GetGuestPassesQueryHandler
    : IRequestHandler<GetGuestPassesQuery, IEnumerable<GuestPassDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetGuestPassesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<GuestPassDto>> Handle(
        GetGuestPassesQuery request,
        CancellationToken cancellationToken)
    {
        var guestPasses = await _unitOfWork
            .Repository<GuestPassEntity>()
            .FindAsync(g => g.MembershipId == request.MembershipId);

        return guestPasses.Select(g => new GuestPassDto
        {
            GuestPassId = g.GuestPassId,
            MembershipId = g.MembershipId,
            IssuedOn = g.IssuedOn,
            UsedOn = g.UsedOn,
            GuestName = g.GuestName
        });
    }
}
