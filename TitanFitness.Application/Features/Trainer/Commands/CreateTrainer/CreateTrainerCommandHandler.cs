using MediatR;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;
using TrainerEntity = TitanFitness.Domain.Entities.Trainer;

namespace TitanFitness.Application.Features.Trainer.Commands.CreateTrainer;

public class CreateTrainerCommandHandler
    : IRequestHandler<CreateTrainerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTrainerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        CreateTrainerCommand request,
        CancellationToken cancellationToken)
    {
        var trainer = new TrainerEntity(
            request.TrainerName,
            request.Email,
            request.Phone,
            request.IsActive);

        await _unitOfWork
           .Repository<TrainerEntity>()
            .AddAsync(trainer);

        await _unitOfWork.SaveChangesAsync();

        return trainer.TrainerId;
    }
}
