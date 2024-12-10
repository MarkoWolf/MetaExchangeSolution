namespace MetaExchange.Core.Models;

public class AggregatedExchangeOrder
{
    public string ExchangeId { get; set; }
    public AggregatedOrder Order { get; set; }
}