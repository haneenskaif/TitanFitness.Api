using MediatR;
using TitanFitness.Application.Features.Members.DTOs;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;
using TitanFitness.Domain.ValueObjects;

namespace TitanFitness.Application.Features.Members.Commands.CreateMember;

public class CreateMemberCommandHandler
    : IRequestHandler<CreateMemberCommand, MemberDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MemberDto> Handle(
        CreateMemberCommand request,
        CancellationToken cancellationToken)
    {
        var membershipNumber = request.MembershipNumber;
        if (string.IsNullOrWhiteSpace(membershipNumber))
        {
            membershipNumber = GenerateMembershipNumber();
        }

        var member = new Member(
            new MembershipNumber(membershipNumber),
            request.FullName,
            request.Email,
            request.Phone,
            request.Address,
            request.JoinedDate,
            request.Photo,
            request.HomeBranchId
        );

        await _unitOfWork.Members.AddAsync(member);

        await _unitOfWork.SaveChangesAsync();

        return new MemberDto
        {
            MemberId = member.MemberId,
            MembershipNumber = member.MembershipNumber.Value,
            FullName = member.FullName,
            Email = member.Email,
            Phone = member.Phone,
            Address = member.Address,
            JoinedDate = member.JoinedDate,
            Photo = member.Photo,
            HomeBranchId = member.HomeBranchId
        };
    }

    private static string GenerateMembershipNumber()
    {
        return $"TF-{Guid.NewGuid().ToString("N")[..7]}";
    }
}
