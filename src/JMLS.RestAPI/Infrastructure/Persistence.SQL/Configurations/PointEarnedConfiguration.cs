using JMLS.Domain.Points;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL.Configurations;

public class PointEarnedConfiguration : IEntityTypeConfiguration<PointEarned>
{
    public void Configure(EntityTypeBuilder<PointEarned> builder)
    {
        builder.ToTable("PointsEarned");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasComment("Unique identifier for this earned points record");

        builder.HasOne(x => x.Activity)
            .WithMany(x => x.PointsEarned)
            .HasForeignKey(x => x.ActivityId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired()
            .HasConstraintName("FK_PointsEarned_Activity");

        builder.HasOne(e => e.Point)
            .WithMany(e => e.PointsEarned)
            .HasForeignKey(e => e.PointId)
            .IsRequired()
            .HasConstraintName("FK_PointsEarned_Point");

        builder.Property(p => p.PointValue)
            .IsRequired()
            .HasColumnName("PointsReward")
            .HasComment("Number of points earned from the associated activity");

        builder.Property(p => p.ExpirationDate)
            .IsRequired(false)
            .HasComment("Expiration date of the earned points, if applicable");

        builder.Property(p => p.DateCreated)
            .HasColumnName("CreatedDateTime")
            .IsRequired()
            .HasComment("Date and time when this earned points record was created");

        builder.Property(p => p.DateModified)
            .HasColumnName("ModifiedDateTime")
            .IsRequired()
            .HasComment("Date and time when this earned points record was last updated");
    }
}