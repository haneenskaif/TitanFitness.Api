using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanFitness.Domain.Entities;

namespace TitanFitness.Infrastructure.Configurations;

public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.ToTable("Memberships");

        builder.HasKey(m => m.MembershipId);

        builder.Property(m => m.PurchaseDate)
            .IsRequired();

        builder.Property(m => m.StartDate)
            .IsRequired();

        builder.Property(m => m.EndDate)
            .IsRequired();

        builder.Property(m => m.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(m => m.MemberId)
            .IsRequired();

        builder.Property(m => m.PlanId)
            .IsRequired();

        builder.OwnsOne(m => m.AgreedTerms, agreedTerms =>
        {
            agreedTerms.Property(a => a.PricePaid)
                .HasColumnName("PricePaid")
                .HasPrecision(18, 2)
                .IsRequired();

            agreedTerms.Property(a => a.DurationInMonths)
                .HasColumnName("AgreedDurationInMonths")
                .IsRequired();

            agreedTerms.Property(a => a.MaximumFreezeDays)
                .HasColumnName("AgreedMaximumFreezeDays")
                .IsRequired();

            agreedTerms.Property(a => a.MaximumFreezes)
                .HasColumnName("AgreedMaximumFreezes")
                .IsRequired();

            agreedTerms.Property(a => a.GuestPassQuota)
                .HasColumnName("AgreedGuestPassQuota")
                .IsRequired();

            agreedTerms.Property(a => a.AccessScope)
                .HasColumnName("AgreedAccessScope")
                .HasConversion<int>()
                .IsRequired();
        });

        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(m => m.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Plan>()
            .WithMany()
            .HasForeignKey(m => m.PlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
