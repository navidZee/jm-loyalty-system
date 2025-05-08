using JMLS.Domain.Customers;
using JMLS.RestAPI.Infrastructure.Persistence.SQL;
using JMLS.RestAPI.Requests;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace JMLS.RestAPI.Services;

public sealed class PointSpentService(
    JmlsDbContext jmlsDbContext,
    IConnectionMultiplexer connectionMultiplexer)
    : CustomerService(jmlsDbContext, connectionMultiplexer)
{
    protected override async Task<Customer> RequestToRequestToPointTransaction(
        RequestToPointTransactionContextDto requestToPointTransactionContextDto,
        CancellationToken cancellationToken)
    {
        var offer = await jmlsDbContext.Offers.FirstOrDefaultAsync(
            d => d.Id == requestToPointTransactionContextDto.RequestToSpentDto!.OfferId,
            cancellationToken);

        requestToPointTransactionContextDto.Customer!.Spent(offer!);

        return requestToPointTransactionContextDto.Customer;
    }
}