using TitanFitness.Domain.Enums;

namespace TitanFitness.Domain.Entities;

public class CheckIn
{
    public int CheckInId { get; private set; }

    public int MemberId { get; private set; }

    public int BranchId { get; private set; }

    public DateTime CheckInDateTime { get; private set; }

    public CheckInResult Result { get; private set; }

    public string? RefusalReason { get; private set; }

    private CheckIn()
    {
    }

    public CheckIn(
        int memberId,
        int branchId,
        DateTime checkInDateTime,
        CheckInResult result,
        string? refusalReason)
    {
        MemberId = memberId;
        BranchId = branchId;
        CheckInDateTime = checkInDateTime;
        Result = result;
        RefusalReason = refusalReason;
    }
}
