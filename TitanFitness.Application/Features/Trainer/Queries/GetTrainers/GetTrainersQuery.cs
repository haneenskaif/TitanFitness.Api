using MediatR;
using TitanFitness.Application.Features.Trainer.TrainerDtos;

namespace TitanFitness.Application.Features.Trainer.Queries.GetTrainers;

public record GetTrainersQuery : IRequest<IEnumerable<TrainerDto>>;
