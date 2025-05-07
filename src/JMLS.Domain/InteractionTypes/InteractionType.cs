namespace JMLS.Domain.InteractionTypes;

public class InteractionType
{
    public int Id { get; set; }
    public required string Key { get; init; }
    public required string Description { get; init; }
    public decimal Amount { get; init; }
}