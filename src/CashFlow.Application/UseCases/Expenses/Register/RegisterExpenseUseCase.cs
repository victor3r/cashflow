using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork) : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var expense = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (PaymentType)request.PaymentType
        };

        _repository.Add(expense);

        _unitOfWork.Commit();

        return new ResponseRegisteredExpenseJson
        {
            Title = request.Title
        };
    }

    private static void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(failure => failure
                .ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
