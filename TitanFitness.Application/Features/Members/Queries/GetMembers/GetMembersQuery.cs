using MediatR;
using TitanFitness.Application.Features.Members.DTOs;

namespace TitanFitness.Application.Features.Members.Queries.GetMembers;

public record GetMembersQuery(
    string? Search,
    int? BranchId,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PagedResult<MemberListDto>>;
