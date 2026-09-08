using MediatR;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Enums;
using TitanFitness.Domain.Interfaces;
using ClassSessionEntity = TitanFitness.Domain.Entities.ClassSession;
using TrainerEntity = TitanFitness.Domain.Entities.Trainer;

namespace TitanFitness.Application.Features.ClassSession.Commands.CreateClassSession;

public class CreateClassSessionCommandHandler
    : IRequestHandler<CreateClassSessionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateClassSessionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreateClassSessionCommand request,
        CancellationToken cancellationToken)
    {
        var studio = await _unitOfWork
            .Repository<Studio>()
            .GetByIdAsync(request.StudioId);

        if (studio is null)
            throw new KeyNotFoundException("Studio not found.");

        if (studio.BranchId != request.BranchId)
            throw new InvalidOperationException(
                "Studio does not belong to the selected branch.");

        if (request.CapacityLimit > studio.Capacity)
            throw new InvalidOperationException(
                "Capacity cannot exceed studio capacity.");

        if (request.DurationInMinutes is not 30 and not 45 and not 60)
            throw new InvalidOperationException(
                "Duration must be 30, 45, or 60 minutes.");

        var trainer = await _unitOfWork
            .Repository<TrainerEntity>()
            .GetByIdAsync(request.TrainerId);

        if (trainer is null)
            throw new KeyNotFoundException("Trainer not found.");

        if (!trainer.IsActive)
            throw new InvalidOperationException(
                "Trainer is not active.");

        var session = new ClassSessionEntity(
            request.ClassName,
            request.BranchId,
            request.StudioId,
            request.TrainerId,
            request.SessionDate,
            request.StartTime,
            request.DurationInMinutes,
            request.CapacityLimit,
            ClassSessionStatus.Open,
            request.Description);

        await _unitOfWork
            .Repository<ClassSessionEntity>()
            .AddAsync(session);

        await _unitOfWork.SaveChangesAsync();

        return session.SessionId;
    }
}