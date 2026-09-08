namespace TitanFitness.Application.Features.Trainer.TrainerDtos;

public class TrainerDto
{
    public int TrainerId { get; set; }
    public string TrainerName { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
}
