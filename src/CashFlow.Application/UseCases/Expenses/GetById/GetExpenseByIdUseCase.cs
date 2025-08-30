﻿using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase(IExpensesReadOnlyRepository repository, IMapper mapper) : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpenseJson> Execute(Guid id)
    {
        var expense = await _repository.GetById(id);

        return expense is null ?
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND) :
            _mapper.Map<ResponseExpenseJson>(expense);
    }
}
