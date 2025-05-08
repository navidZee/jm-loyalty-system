using JMLS.Domain.Offers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL.Configurations;

public class AuditLogConfigurations : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");
        builder.HasIndex(p => p.Id).IsUnique();
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedNever();;
        
        builder.Property(p => p.DateCreated).HasColumnName("CreatedDateTime").IsRequired();
        builder.Property(p => p.DateModified).HasColumnName("ModifiedDateTime").IsRequired(false);
    }
}