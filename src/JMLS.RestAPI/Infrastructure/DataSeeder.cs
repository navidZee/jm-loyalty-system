using JMLS.Domain.Activities;
using JMLS.Domain.Offers;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using Microsoft.EntityFrameworkCore;

namespace JMLS.RestAPI.Infrastructure;

public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<JmlsDbContext>();

        // Ensure database is created and migrations are applied
        await dbContext.Database.MigrateAsync();

        // Check if we already have data
        if (await dbContext.Activities.AnyAsync() || await dbContext.Offers.AnyAsync())
            return;

        // Add sample activities
        var activities = new List<Activity>
        {
            new("Activity 1", ActivityType.Purchases, 100, "Description 1", null),
            new("Activity 2", ActivityType.SocialMedia, 50, "Description 2", null)
        };
        await dbContext.Activities.AddRangeAsync(activities);

        // Add sample offers
        var offers = new List<Offer>
        {
            new(20, OfferType.Flat, 100000, 1, OfferReferenceType.Catalog),
            new(40, OfferType.Percentage, 100000, 1, OfferReferenceType.Catalog)
        };
        await dbContext.Offers.AddRangeAsync(offers);

        await dbContext.SaveChangesAsync();
    }
}