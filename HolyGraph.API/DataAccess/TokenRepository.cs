
using HolyGraph.API.Interfaces;

namespace HolyGraph.API;

public class TokenRepository : ITokenRepository
{
    private readonly INeo4jGraphRepository _neoGraph;
    public TokenRepository(INeo4jGraphRepository neoGraph)
    {
        _neoGraph = neoGraph;
    }   
    public async Task<IEnumerable<TokenDto>> GetArmatTokensAsync(string armat)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Request: Armat -> GetArmatTokensAsync: {armat}");
        
        var query =
            $@"
            MATCH(t:Token)
            WHERE t.armat = '{armat}'
            RETURN t 
            ";

        return await _neoGraph.RunQueryAsync<TokenDto>(query);
    }

    public async Task<IEnumerable<TokenDto>> GetArmatTokensAsync(int armatId)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"Request: Armat -> GetArmatTokensAsync: {armatId}");
        
        var query =
            $@"
            MATCH(t:Token)
            WHERE t.armatID = {armatId} 
            RETURN t 
            ";

        return await _neoGraph.RunQueryAsync<TokenDto>(query);
    }
}
