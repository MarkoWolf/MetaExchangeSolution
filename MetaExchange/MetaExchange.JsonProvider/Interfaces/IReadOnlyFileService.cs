namespace MetaExchange.JsonProvider.Interfaces;

public interface IReadOnlyFileService
{
    List<T> ReadAllFromFolder<T>();
}