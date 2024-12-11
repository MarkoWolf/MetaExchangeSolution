using MetaExchange.Application.Interfaces;
using MetaExchange.Application.Mappers;
using MetaExchange.Application.Models;
using MetaExchange.Core.Extensions;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Models;
using MetaExchange.JsonProvider.Interfaces;

namespace MetaExchange.Application.Services;

public class OrderBookService(IReadOnlyExchangeJsonRepository repository, IBuyService buyService, ISellService sellService) : IOrderBookService
{
    private readonly IBuyService _buyService = buyService.NotNull(nameof(buyService));
    private readonly ISellService _sellService = sellService.NotNull(nameof(sellService));
    private readonly IReadOnlyExchangeJsonRepository _repository = repository.NotNull(nameof(repository));

    public List<OrderExecutionPlan> GetBuyExecutePlan(decimal amount)
    {
        List<Exchange> allExchanges = _repository.GetAll();
        List<OrderExecutionResult> result = _buyService.GetExecuteOrders(amount, allExchanges);
        return result.Map();
    }
    
    public List<OrderExecutionPlan> GetSellExecutePlan(decimal amount)
    {
        List<Exchange> allExchanges = _repository.GetAll();
        List<OrderExecutionResult> result = _sellService.GetExecuteOrders(amount, allExchanges);
        return result.Map();
    }
}