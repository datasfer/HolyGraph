
namespace HolyGraph.API.Interfaces;

public interface INeo4jGraphRepository
{
    Task<string> RunQueryAsync(string query);
}