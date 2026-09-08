using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Plans");

        builder.HasKey(p => p.PlanId);

        builder.Property(p => p.PlanName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.DurationInMonths)
            .IsRequired();

        builder.Property(p => p.MaximumFreezeDays)
            .IsRequired();

        builder.Property(p => p.MaximumFreezes)
            .IsRequired();

        builder.Property(p => p.GuestPassQuota)
            .IsRequired();

        builder.Property(p => p.AccessScope)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.IsPublished)
            .IsRequired();
    }
}
