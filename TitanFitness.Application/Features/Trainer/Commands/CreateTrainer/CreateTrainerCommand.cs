using MediatR;

namespace TitanFitness.Application.Features.Trainer.Commands.CreateTrainer;

public record CreateTrainerCommand(
    string TrainerName,
    string? Email,
    string? Phone,
    bool IsActive
) : IRequest<int>;
