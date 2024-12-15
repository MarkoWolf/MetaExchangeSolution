namespace MetaExchange.Core.Models;

public class AggregatedExchangeOrder
{
    public string ExchangeId { get; set; } = String.Empty;
    public AggregatedOrder Order { get; set; } = new AggregatedOrder();
}