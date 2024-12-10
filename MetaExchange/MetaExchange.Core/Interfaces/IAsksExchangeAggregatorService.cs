using MetaExchange.Core.Models;

namespace MetaExchange.Core.Interfaces;

public interface IAsksExchangeAggregatorService
{
    AggregatedExchanges GetSortedAsks(List<Exchange> exchanges);
}