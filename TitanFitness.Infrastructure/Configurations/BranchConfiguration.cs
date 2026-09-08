using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(b => b.BranchId);

        builder.Property(b => b.BranchName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.Address)
            .HasMaxLength(200);

        builder.Property(b => b.OpeningTime)
            .IsRequired();

        builder.Property(b => b.ClosingTime)
            .IsRequired();
    }
}
