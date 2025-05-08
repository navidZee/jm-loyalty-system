using JMLS.Domain.Points;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL.Configurations;

public class PointConfiguration : IEntityTypeConfiguration<Point>
{
    public void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.ToTable("Points");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasComment("Unique identifier for the points record");

        builder.Property(p => p.CustomerId)
            .IsRequired()
            .HasComment("Identifier of the customer who owns this points record");

        builder.Property(p => p.Balance)
            .HasDefaultValue(0)
            .IsRequired()
            .HasComment("Total available points for the customer");

        builder.HasOne(e => e.Customer)
            .WithOne(e => e.Point)
            .HasForeignKey<Point>(e => e.CustomerId)
            .IsRequired()
            .HasConstraintName("FK_Points_Customer")
            .OnDelete(DeleteBehavior.NoAction); // Optional: consistent delete behavior

        builder.HasMany(p => p.PointsEarned)
            .WithOne(e => e.Point)
            .HasForeignKey(p => p.PointId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired()
            .HasConstraintName("FK_PointsEarned_Point");

        builder.HasMany(p => p.PointsSpent)
            .WithOne(e => e.Point)
            .HasForeignKey(p => p.PointId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired()
            .HasConstraintName("FK_PointsSpent_Point");

        builder.Property(p => p.DateCreated)
            .HasColumnName("CreatedDateTime")
            .IsRequired()
            .HasComment("Date and time when this points record was created");

        builder.Property(p => p.DateModified)
            .HasColumnName("ModifiedDateTime")
            .IsRequired()
            .HasComment("Date and time when this points record was last updated");
    }
}