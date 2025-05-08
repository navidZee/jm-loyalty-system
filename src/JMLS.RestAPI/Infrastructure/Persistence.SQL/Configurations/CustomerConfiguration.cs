using JMLS.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasComment("Unique identifier for the customer");

        builder.Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(255)
            .HasComment("Customer username");

        builder.HasIndex(p => p.Username)
            .IsUnique();
        
        builder.Property(p => p.DateCreated)
            .HasColumnName("CreatedDateTime")
            .IsRequired()
            .HasComment("Date and time when the customer record was created");

        builder.Property(p => p.DateModified)
            .HasColumnName("ModifiedDateTime")
            .IsRequired()
            .HasComment("Date and time when the customer record was last modified");
    }
}