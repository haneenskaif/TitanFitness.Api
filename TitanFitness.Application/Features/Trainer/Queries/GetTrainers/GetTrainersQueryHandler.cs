using MediatR;
using TitanFitness.Application.Features.Trainer.TrainerDtos;
using TitanFitness.Domain.Entities;
using TitanFitness.Domain.Interfaces;
using TrainerEntity = TitanFitness.Domain.Entities.Trainer;

namespace TitanFitness.Application.Features.Trainer.Queries.GetTrainers;

public class GetTrainersQueryHandler
    : IRequestHandler<GetTrainersQuery, IEnumerable<TrainerDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTrainersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TrainerDto>> Handle(
        GetTrainersQuery request,
        CancellationToken cancellationToken)
    {
        var trainers = await _unitOfWork
            .Repository<TrainerEntity>()
            .GetAllAsync();

        return trainers.Select(t => new TrainerDto
        {
            TrainerId = t.TrainerId,
            TrainerName = t.TrainerName,
            Email = t.Email,
            Phone = t.Phone,
            IsActive = t.IsActive
        });
    }
}
