using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.BookingId);

        builder.Property(b => b.SessionId)
            .IsRequired();

        builder.Property(b => b.MemberId)
            .IsRequired();

        builder.Property(b => b.BookedOn)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(b => b.WaitlistPosition)
            .IsRequired(false);

        builder.Property(b => b.Notes)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.HasOne<ClassSession>()
            .WithMany()
            .HasForeignKey(b => b.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(b => b.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(b => new { b.SessionId, b.MemberId })
            .IsUnique();
    }
}
