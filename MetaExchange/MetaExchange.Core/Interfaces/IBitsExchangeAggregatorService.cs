using MetaExchange.Core.Models;

namespace MetaExchange.Core.Interfaces;

public interface IBitsExchangeAggregatorService
{
    AggregatedExchanges GetSortedBids(List<Exchange> exchanges);
}