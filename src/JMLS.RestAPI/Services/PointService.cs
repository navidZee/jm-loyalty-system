using JMLS.Domain.Points;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using JMLS.RestAPI.Requests;
using Microsoft.EntityFrameworkCore;

namespace JMLS.RestAPI.Services;

public interface IPointService
{
    public Task RequestToEarn(RequestToEarnDto requestToEarnDto, CancellationToken cancellationToken);

    public Task<int> GetCustomerPointsBalance(int customerId, CancellationToken cancellationToken);
}

public class PointService(JmlsDbContext jmlsDbContext) : IPointService
{
    public async Task RequestToEarn(RequestToEarnDto requestToEarnDto,
        CancellationToken cancellationToken)
    {
        var point = await jmlsDbContext.Points.Include(d => d.PointsEarned).Include(d => d.PointsSpent)
            .FirstOrDefaultAsync(d => d.CustomerId == requestToEarnDto.CustomerId,
                cancellationToken);
        point ??= await CreateCustomerPoint(requestToEarnDto.CustomerId, cancellationToken);

        var activity =
            await jmlsDbContext.Activities.FirstOrDefaultAsync(d => d.Id == requestToEarnDto.ActivityId,
                cancellationToken);

        point.Earned(activity!, requestToEarnDto.ReferenceId);
        jmlsDbContext.Points.Update(point);
        await jmlsDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCustomerPointsBalance(int customerId, CancellationToken cancellationToken)
    {
        return await jmlsDbContext.Points.AsNoTracking().Where(d => d.CustomerId == customerId).Select(d => d.Balance)
            .FirstOrDefaultAsync(cancellationToken);
    }

    private async Task<Point> CreateCustomerPoint(int customerId, CancellationToken cancellationToken)
    {
        var point = new Point(customerId);

        jmlsDbContext.Points.Add(point);
        await jmlsDbContext.SaveChangesAsync(cancellationToken);

        return point;
    }
}