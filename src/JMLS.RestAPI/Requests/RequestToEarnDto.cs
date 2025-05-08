namespace JMLS.RestAPI.Requests;

public class RequestToEarnDto
{
    public int ActivityId { get; init; }
    public int ReferenceId { get; init; }
    public int CustomerId { get; set; }
}

// public class RequestToEarnDto
// {
//     public int ActivityId { get; init; }
//     public int ActivityReferenceId { get; init; }
//     public int CustomerId { get; set; }
// }