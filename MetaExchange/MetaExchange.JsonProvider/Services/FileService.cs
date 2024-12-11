using MetaExchange.Core.Extensions;
using MetaExchange.JsonProvider.Configurations;
using MetaExchange.JsonProvider.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace MetaExchange.JsonProvider.Services;

public class FileService(IOptions<JsonProviderOptions> options) : IReadOnlyFileService
{
    private readonly JsonProviderOptions _options = options.NotNull(nameof(options)).Value;
    private readonly string _searchPattern = "*.json";

    public List<T> ReadAllFromFolder<T>()
    {
        var folderPath =  Path.Combine(AppContext.BaseDirectory, _options.FilePath);
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            return [];
        }

        var files = GetJsonFiles(folderPath);
        
        if (!files.Any())
        {
            return [];
        }

        return files.SelectMany(file => ReadFromFile<T>(file)).ToList();
    }

    private IEnumerable<string> GetJsonFiles(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            return [];
        }

        return Directory.GetFiles(folderPath, _searchPattern);
    }

    private IEnumerable<T> ReadFromFile<T>(string filePath)
    {
        try
        {
            var jsonContent = File.ReadAllText(filePath);

            if (!IsJsonValid<T>(jsonContent))
            {
                return [];
            }

            var deserializedObject = JsonConvert.DeserializeObject<T>(jsonContent);
            return deserializedObject != null ? new List<T> { deserializedObject } : new List<T> { };
        }
        catch
        {
            return [];
        }
    }

    private bool IsJsonValid<T>(string jsonContent)
    {
        var generator = new JSchemaGenerator();
        var schema = generator.Generate(typeof(T));

        var jsonObject = JObject.Parse(jsonContent);
        return jsonObject.IsValid(schema, out IList<string> _);
    }
}