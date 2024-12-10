using System.Reflection;

namespace MetaExchange.JsonProvider.Tests.TestUtlilites;

public static class TestHelper
{
    public static TemporaryFolder CreateTemporaryFolder()
    {
        return new TemporaryFolder();
    }

    public static void CreateJsonFile(string folderPath, string fileName, string content)
    {
        var filePath = Path.Combine(folderPath, fileName);
        File.WriteAllText(filePath, content);
    }
    
    public static void ExtractEmbeddedResource(string resourceName, string outputPath)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var resourceStream = assembly.GetManifestResourceStream(resourceName);
        if (resourceStream == null)
        {
            throw new FileNotFoundException($"Embedded resource {resourceName} not found.");
        }

        using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
        resourceStream.CopyTo(fileStream);
    }
}