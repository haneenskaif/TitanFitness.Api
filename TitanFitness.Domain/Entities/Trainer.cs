namespace TitanFitness.Domain.Entities;

public class Trainer
{
    public int TrainerId { get; private set; }

    public string TrainerName { get; private set; } = null!;

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public bool IsActive { get; private set; }

    private Trainer()
    {
    }

    public Trainer(
        string trainerName,
        string? email,
        string? phone,
        bool isActive)
    {
        TrainerName = trainerName;
        Email = email;
        Phone = phone;
        IsActive = isActive;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}
