using MediatR;
using TitanFitness.Application.Features.Members.DTOs;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Enums;
using TitanFitness.Domain.Interfaces;
using CheckInEntity = TitanFitness.Domain.Entities.CheckIn;

namespace TitanFitness.Application.Features.Members.Queries.GetMembers;

public class GetMembersQueryHandler
    : IRequestHandler<GetMembersQuery, PagedResult<MemberListDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMembersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<MemberListDto>> Handle(
        GetMembersQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber < 1
            ? 1
            : request.PageNumber;

        var pageSize = request.PageSize < 1
            ? 10
            : request.PageSize;

        var members = await _unitOfWork.Repository<Member>()
            .GetAllAsync();

        var branches = await _unitOfWork.Repository<Branch>()
            .GetAllAsync();

        var memberships = await _unitOfWork.Repository<Membership>()
            .GetAllAsync();

        var checkIns = await _unitOfWork.Repository<CheckInEntity>() 
            .GetAllAsync();

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var memberList = members.Select(member =>
        {
            var branch = branches.FirstOrDefault(
                b => b.BranchId == member.HomeBranchId);

            var membership = memberships
                .Where(m => m.MemberId == member.MemberId)
                .OrderByDescending(m => m.StartDate)
                .FirstOrDefault();

            var currentMembership = memberships
                .Where(m =>
                    m.MemberId == member.MemberId &&
                    m.StartDate <= today &&
                    m.EndDate >= today &&
                    m.Status != MembershipStatus.Cancelled)
                .OrderByDescending(m => m.StartDate)
                .FirstOrDefault();

            var status = currentMembership?.Status
                         ?? membership?.Status;

            var lastVisit = checkIns
                .Where(c =>
                    c.MemberId == member.MemberId &&
                    c.Result == CheckInResult.Admitted)
                .OrderByDescending(c => c.CheckInDateTime)
                .Select(c => (DateTime?)c.CheckInDateTime)
                .FirstOrDefault();

            return new MemberListDto
            {
                MemberId = member.MemberId,

                MembershipNumber =
                    member.MembershipNumber.Value,

                FullName = member.FullName,

                Status = status?.ToString() ?? "No Membership",

                BranchName = branch?.BranchName ?? string.Empty,

                LastVisit = lastVisit
            };
        });

        var search = request.Search?.Trim();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();

            memberList = memberList.Where(member =>
                member.FullName.ToLower().Contains(search) ||
                member.MembershipNumber.ToLower().Contains(search) ||
                member.Status.ToLower().Contains(search) ||
                member.BranchName.ToLower().Contains(search) ||
                (member.LastVisit.HasValue &&
                 member.LastVisit.Value
                     .ToString("yyyy-MM-dd HH:mm")
                     .ToLower()
                     .Contains(search))
            );
        }

        if (request.BranchId.HasValue)
        {
            var selectedBranch = branches.FirstOrDefault(
                b => b.BranchId == request.BranchId.Value);

            if (selectedBranch != null)
            {
                memberList = memberList.Where(
                    member => member.BranchName == selectedBranch.BranchName);
            }
            else
            {
                memberList = Enumerable.Empty<MemberListDto>();
            }
        }

        var filteredMembers = memberList
            .OrderBy(member => member.FullName)
            .ToList();

        var totalCount = filteredMembers.Count;

        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(
                totalCount / (double)pageSize);

        var items = filteredMembers
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<MemberListDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }
}