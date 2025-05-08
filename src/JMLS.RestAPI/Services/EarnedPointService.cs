using JMLS.Domain.Customers;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using JMLS.RestAPI.Requests;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace JMLS.RestAPI.Services;

public sealed class PointEarnedService(
    JmlsDbContext jmlsDbContext,
    IConnectionMultiplexer connectionMultiplexer)
    : CustomerService(jmlsDbContext, connectionMultiplexer)
{
    protected override async Task<Customer> RequestToRequestToPointTransaction(
        RequestToPointTransactionContextDto requestToPointTransactionContextDto,
        CancellationToken cancellationToken)
    {
        var activity = await jmlsDbContext.Activities.FirstOrDefaultAsync(
            d => d.Id == requestToPointTransactionContextDto.RequestToEarnDto!.ActivityId,
            cancellationToken);

        requestToPointTransactionContextDto.Customer!.Earned(activity!,
            requestToPointTransactionContextDto.RequestToEarnDto!.ReferenceId);

        return requestToPointTransactionContextDto.Customer;
    }
}