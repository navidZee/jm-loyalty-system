namespace JMLS.RestAPI.Requests;

public class UpdateReviewRequest
{
    public Guid AuthorId { get; init; }

    public Guid MovieId { get; init; }

    public int Stars { get; init; }
}
