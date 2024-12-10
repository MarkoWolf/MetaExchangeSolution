namespace MetaExchange.Core.Models;

public class AggregatedExchanges
{
    public List<AggregatedFund> Funds { get; set; } = [];
    public List<AggregatedExchangeOrder> AggregatedOrders { get; set; } = [];
}