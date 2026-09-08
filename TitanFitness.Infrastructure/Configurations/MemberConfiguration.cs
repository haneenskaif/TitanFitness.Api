using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(m => m.MemberId);

        builder.Property(m => m.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.Email)
            .HasMaxLength(100);

        builder.Property(m => m.Phone)
            .HasMaxLength(20);

        builder.Property(m => m.Address)
            .HasMaxLength(200);

        builder.Property(m => m.JoinedDate)
            .IsRequired();

        builder.Property(m => m.Photo);

        builder.Property(m => m.HomeBranchId)
            .IsRequired();

        builder.Property(m => m.MembershipNumber)
            .HasConversion(
                membershipNumber => membershipNumber.Value,
                value => new TitanFitness.Domain.ValueObjects.MembershipNumber(value))
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(m => m.MembershipNumber)
            .IsUnique();

        builder.HasOne<Branch>()
            .WithMany()
            .HasForeignKey(m => m.HomeBranchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}