using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class GuestPassConfiguration : IEntityTypeConfiguration<GuestPass>
{
    public void Configure(EntityTypeBuilder<GuestPass> builder)
    {
        builder.ToTable("GuestPasses");

        builder.HasKey(g => g.GuestPassId);

        builder.Property(g => g.MembershipId)
            .IsRequired();

        builder.Property(g => g.IssuedOn)
            .IsRequired();

        builder.Property(g => g.UsedOn)
            .IsRequired(false);

        builder.Property(g => g.GuestName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasOne<Membership>()
            .WithMany()
            .HasForeignKey(g => g.MembershipId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
