using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class CheckInConfiguration : IEntityTypeConfiguration<CheckIn>
{
    public void Configure(EntityTypeBuilder<CheckIn> builder)
    {
        builder.ToTable("CheckIns");

        builder.HasKey(c => c.CheckInId);

        builder.Property(c => c.MemberId)
            .IsRequired();

        builder.Property(c => c.BranchId)
            .IsRequired();

        builder.Property(c => c.CheckInDateTime)
            .IsRequired();

        builder.Property(c => c.Result)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(c => c.RefusalReason)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(c => c.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Branch>()
            .WithMany()
            .HasForeignKey(c => c.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}