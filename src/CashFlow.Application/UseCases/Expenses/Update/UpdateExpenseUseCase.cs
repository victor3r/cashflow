using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase(
    IExpensesUpdateOnlyRepository repository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IUpdateExpenseUseCase
{
    private readonly IExpensesUpdateOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task Execute(Guid id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id) ??
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    private static void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(failure => failure
                .ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
