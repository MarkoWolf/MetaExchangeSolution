namespace MetaExchange.JsonProvider.Tests.TestUtlilites;

public class TemporaryFolder : IDisposable
{
    public string FolderPath { get; }

    public TemporaryFolder()
    {
        FolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(FolderPath);
    }

    public void Dispose()
    {
        if (Directory.Exists(FolderPath))
        {
            Directory.Delete(FolderPath, true);
        }
    }
}