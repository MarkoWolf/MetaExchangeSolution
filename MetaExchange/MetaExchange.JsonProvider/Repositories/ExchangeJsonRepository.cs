using MetaExchange.Core.Extensions;
using MetaExchange.Core.Models;
using MetaExchange.JsonProvider.Extentsions;
using MetaExchange.JsonProvider.Interfaces;
using MetaExchange.JsonProvider.Models;

namespace MetaExchange.JsonProvider.Repositories;

public class ExchangeJsonRepository(IReadOnlyFileService fileService) : IReadOnlyExchangeJsonRepository
{
    private readonly IReadOnlyFileService _fileService = fileService.NotNull();

    public List<Exchange> GetAll()
    {
        List<JsonExchange> jsonExchanges =  _fileService.ReadAllFromFolder<JsonExchange>();
        
        if (jsonExchanges is null)
        {
            return [];
        }
        
        return jsonExchanges.Select(jsonExchange => jsonExchange.MapToDomain()).ToList();
    }
}