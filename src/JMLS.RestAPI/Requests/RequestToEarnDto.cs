using JMLS.Domain.Customers;

namespace JMLS.RestAPI.Requests;

public class RequestToPointTransactionContextDto
{
    public RequestToEarnDto? RequestToEarnDto { get; init; }
    public RequestToSpentDto? RequestToSpentDto { get; init; }
    public Customer? Customer { get; set; }
    public int CustomerId { get; set; }
}

public class RequestToEarnDto
{
    public int ActivityId { get; init; }
    public int ReferenceId { get; init; }
}

public class RequestToSpentDto
{
    public int OfferId { get; init; }
}