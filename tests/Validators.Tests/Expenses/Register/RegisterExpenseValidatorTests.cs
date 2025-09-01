using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Error_Empty_Title(string? title)
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var validator = new ExpenseValidator();

        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error =>
            error.ErrorMessage == ResourceErrorMessages.REQUIRED_TITLE);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Invalid_Amount(decimal amount)
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var validator = new ExpenseValidator();

        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(error =>
            error.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }

    [Fact]
    public void Error_Future_Date()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var validator = new ExpenseValidator();

        request.Date = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        request.Date.Should().BeAfter(DateTime.UtcNow);
        result.Errors.Should().ContainSingle().And.Contain(error =>
            error.ErrorMessage == ResourceErrorMessages.DATE_CANNOT_BE_IN_THE_FUTURE);
    }

    [Fact]
    public void Error_Invalid_Payment_Type()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var validator = new ExpenseValidator();

        request.PaymentType = (PaymentType)4;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        request.PaymentType.Should().Be((PaymentType)4);
        result.Errors.Should().ContainSingle().And.Contain(error =>
            error.ErrorMessage == ResourceErrorMessages.INVALID_PAYMENT_TYPE);
    }
}
