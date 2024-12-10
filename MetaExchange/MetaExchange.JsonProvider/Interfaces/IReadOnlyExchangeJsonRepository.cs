using MetaExchange.Core.Models;

namespace MetaExchange.JsonProvider.Interfaces;

public interface IReadOnlyExchangeJsonRepository
{
    List<Exchange> GetAll();
}