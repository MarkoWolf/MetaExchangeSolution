using MetaExchange.Core.Extensions;
using MetaExchange.JsonProvider.Configurations;
using MetaExchange.JsonProvider.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MetaExchange.JsonProvider.Services;

public class FileService(IOptions<JsonProviderOptions> options, ILogger<FileService> logger) : IReadOnlyFileService
{
    private readonly ILogger<FileService> _logger = logger.NotNull(nameof(logger));
    private readonly JsonProviderOptions _options = options.NotNull(nameof(options)).Value;
    private readonly string _searchPattern = "*.json";

    public List<T> ReadAllFromFolder<T>()
    {
        if (string.IsNullOrWhiteSpace(_options.FilePath))
        {
            _logger.LogError("The folder path is empty.");
            return [];
        }
        
        var folderPath =  Path.Combine(AppContext.BaseDirectory, _options.FilePath);
        
        var files = GetJsonFiles(folderPath);
        
        if (!files.Any())
        {
            _logger.LogError("No files found in folder {folderPath}.", folderPath);
            return [];
        }

        return files.SelectMany(file => ReadFromFile<T>(file)).ToList();
    }

    private IEnumerable<string> GetJsonFiles(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            _logger.LogError("The folder path {folderPath} does not exist.", folderPath);
            return [];
        }

        return Directory.GetFiles(folderPath, _searchPattern);
    }

    private IEnumerable<T> ReadFromFile<T>(string filePath)
    {
        try
        {
            var jsonContent = File.ReadAllText(filePath);

            var deserializedObject = JsonConvert.DeserializeObject<T>(jsonContent);
            return deserializedObject != null ? new List<T> { deserializedObject } : new List<T> { };
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "The file {filePath} is not valid.", filePath);
            return [];
        }
    }
}