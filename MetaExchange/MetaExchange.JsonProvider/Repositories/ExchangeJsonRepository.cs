using MetaExchange.Core.Extensions;
using MetaExchange.Core.Models;
using MetaExchange.JsonProvider.Interfaces;
using MetaExchange.JsonProvider.Mappers;
using MetaExchange.JsonProvider.Models;
using Microsoft.Extensions.Logging;

namespace MetaExchange.JsonProvider.Repositories;

public class ExchangeJsonRepository(IReadOnlyFileService fileService, ILogger<ExchangeJsonRepository> logger) : IReadOnlyExchangeJsonRepository
{
    private readonly  ILogger<ExchangeJsonRepository> _logger = logger.NotNull(nameof(logger));
    private readonly IReadOnlyFileService _fileService = fileService.NotNull(nameof(fileService));

    public List<Exchange> GetAll()
    {
        List<JsonExchange> jsonExchanges =  _fileService.ReadAllFromFolder<JsonExchange>();
        
        if (jsonExchanges is null)
        {
            _logger.LogInformation("No exchanges found");
            return [];
        }
        
        return jsonExchanges.Select(jsonExchange => jsonExchange.Map()).ToList();
    }
}