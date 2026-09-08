using MediatR;

namespace TitanFitness.Application.Features.CheckIn.Commands.CreateCheckIn;

public record CreateCheckInCommand(
    int MemberId,
    int BranchId
) : IRequest<int>;