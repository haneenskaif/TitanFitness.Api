using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class FreezeConfiguration : IEntityTypeConfiguration<Freeze>
{
    public void Configure(EntityTypeBuilder<Freeze> builder)
    {
        builder.ToTable("Freezes");

        builder.HasKey(f => f.FreezeId);

        builder.Property(f => f.MembershipId)
            .IsRequired();

        builder.Property(f => f.StartDate)
            .IsRequired();

        builder.Property(f => f.EndDate)
            .IsRequired();

        builder.Property(f => f.DurationInMonths)
            .IsRequired();

        builder.Property(f => f.Reason)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(f => f.AdditionalNotes)
            .HasMaxLength(200);

        builder.Property(f => f.RequestedOn)
            .IsRequired();

        builder.HasOne<Membership>()
            .WithMany()
            .HasForeignKey(f => f.MembershipId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}