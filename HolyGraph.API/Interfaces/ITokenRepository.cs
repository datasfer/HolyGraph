namespace HolyGraph.API;

public interface ITokenRepository
{
    Task<IEnumerable<TokenDto>> GetArmatTokensAsync(int armatId);
    Task<IEnumerable<TokenDto>> GetArmatTokensAsync(string armat);
}
