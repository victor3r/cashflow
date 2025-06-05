﻿using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty()
            .WithMessage(ResourceErrorMessages.REQUIRED_TITLE);

        RuleFor(expense => expense.Amount).GreaterThan(0)
            .WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(ResourceErrorMessages.DATE_CANNOT_BE_IN_THE_FUTURE);

        RuleFor(expense => expense.PaymentType)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.INVALID_PAYMENT_TYPE);
    }
}
