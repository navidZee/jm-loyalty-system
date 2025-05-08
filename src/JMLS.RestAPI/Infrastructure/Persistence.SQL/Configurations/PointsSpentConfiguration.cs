using JMLS.Domain.Points;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL.Configurations;

public class PointsSpentConfiguration : IEntityTypeConfiguration<PointSpent>
{
    public void Configure(EntityTypeBuilder<PointSpent> builder)
    {
        builder.ToTable("PointsSpent");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasComment("Unique identifier for the point account");

        builder
            .HasOne(x => x.Offer)
            .WithMany(x => x.PointsSpent)
            .HasForeignKey(x => x.OfferId)
            .IsRequired();
        
        builder
            .HasOne(e => e.Point)
            .WithMany(e => e.PointsSpent)
            .HasForeignKey(e => e.PointId)
            .IsRequired();
        
        builder.Property(p => p.PointValue)
            .IsRequired()
            .HasColumnName("PointsCost")
            .HasComment("Current point balance for the customer");
        
        builder.Property(p => p.DateCreated)
            .HasColumnName("CreatedDateTime")
            .IsRequired()
            .HasComment("Date and time when the point account was created");

        builder.Property(p => p.DateModified)
            .HasColumnName("ModifiedDateTime")
            .IsRequired()
            .HasComment("Date and time when the point account was last modified");
    }
}