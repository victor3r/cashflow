using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        var request = new RequestRegisterExpenseJson
        {
            Title = "Valid Expense",
            Description = "This is a valid expense description.",
            Amount = 100.00m,
            Date = DateTime.Now.AddDays(-1),
            PaymentType = PaymentType.CreditCard
        };

        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
