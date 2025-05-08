using JMLS.Domain.Activities;
using JMLS.Domain.Customers;
using JMLS.Domain.Offers;
using JMLS.Domain.Points;
using Microsoft.EntityFrameworkCore;

namespace JMLS.RestAPI.Infrastructure.Persistence.SQL;

public sealed class JmlsDbContext(DbContextOptions<JmlsDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Activity> Activities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var relationship in modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JmlsDbContext).Assembly);
    }
}