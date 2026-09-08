using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable("Trainers");

        builder.HasKey(t => t.TrainerId);

        builder.Property(t => t.TrainerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Email)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(t => t.Phone)
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(t => t.IsActive)
            .IsRequired();
    }
}