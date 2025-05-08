using JMLS.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("Activities");
        
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd()
            .HasComment("Unique identifier for the activity");
        
        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(512)
            .HasComment("Title or name of the activity");
        
        builder.Property(a => a.ActivityType)
            .HasConversion(v => v.ToString(), v => Enum.Parse<ActivityType>(v))
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("Type or category of the activity");
        
        builder.Property(a => a.PointsReward)
            .IsRequired()
            .HasComment("Number of points rewarded for completing the activity");
        
        builder.Property(a => a.Description)
            .HasMaxLength(2048)
            .HasComment("Optional detailed description of the activity");
        
        builder.Property(a => a.ExpirationPeriod)
            .IsRequired(false)
            .HasComment("Optional time span after which the earned points from this activity expire");
    
        builder.HasMany(a => a.PointsEarned)
            .WithOne(e => e.Activity)
            .HasForeignKey(e => e.ActivityId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired()
            .HasConstraintName("FK_PointsEarned_Activity");
        
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