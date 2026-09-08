using MediatR;
using TitanFitness.Application.Features.Members.DTOs;

namespace TitanFitness.Application.Features.Members.Commands.CreateMember;

public record CreateMemberCommand(
    string? MembershipNumber,
    string FullName,
    string? Email,
    string? Phone,
    string? Address,
    DateOnly JoinedDate,
    string? Photo,
    int HomeBranchId
) : IRequest<MemberDto>;