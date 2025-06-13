using CashFlow.Application.UseCases.Expenses.Register;
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

        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Title_Empty()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var validator = new RegisterExpenseValidator();

        request.Title = "";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        request.Title.Should().BeEmpty();
        result.Errors.Should().ContainSingle().And.Contain(error =>
            error.ErrorMessage == ResourceErrorMessages.REQUIRED_TITLE);
    }
}
