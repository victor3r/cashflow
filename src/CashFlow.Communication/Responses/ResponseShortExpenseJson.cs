namespace CashFlow.Communication.Responses;
public class ResponseShortExpenseJson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public decimal Amount { get; set; }

}
