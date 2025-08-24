using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase(IExpensesRepository repository, IMapper mapper) : IGetExpenseByIdUseCase
{
    private readonly IExpensesRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpenseJson> Execute(Guid id)
    {
        var expense = await _repository.GetById(id);

        return _mapper.Map<ResponseExpenseJson>(expense);
    }
}
