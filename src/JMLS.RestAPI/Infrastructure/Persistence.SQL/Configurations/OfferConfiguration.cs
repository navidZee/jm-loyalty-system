using JMLS.Domain.Offers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL.Configurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasComment("Unique identifier for the offer");

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(1024)
            .HasComment("Unique code representing this offer");

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasComment("Value or discount amount associated with the offer");

        builder.Property(p => p.PointsCost)
            .IsRequired()
            .HasComment("Points required by the customer to redeem this offer");

        builder.Property(b => b.Type)
            .HasConversion(v => v.ToString(), v => Enum.Parse<OfferType>(v))
            .IsRequired()
            .HasMaxLength(250)
            .HasComment("Classification or type of the offer");

        builder.Property(b => b.ReferenceId)
            .IsRequired()
            .HasComment("Identifier of the entity this offer is linked to (e.g., product ID)");

        builder.Property(b => b.ReferenceType)
            .HasConversion(v => v.ToString(), v => Enum.Parse<OfferReferenceType>(v))
            .IsRequired()
            .HasMaxLength(250)
            .HasComment("Type of the entity referenced by this offer (e.g., product, category)");

        builder.Property(p => p.DateCreated)
            .HasColumnName("CreatedDateTime")
            .IsRequired()
            .HasComment("Timestamp when the offer was created");

        builder.Property(p => p.DateModified)
            .HasColumnName("ModifiedDateTime")
            .IsRequired()
            .HasComment("Timestamp when the offer was last updated");
    }
}