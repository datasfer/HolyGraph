
namespace HolyGraph.API.Interfaces;

public interface INeo4jGraphRepository
{
    Task<string> RunQueryAsync(string query);
    Task<IEnumerable<T>> RunQueryAsync<T>(string query);
}