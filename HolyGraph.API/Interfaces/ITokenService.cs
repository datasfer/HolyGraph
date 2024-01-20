using HolyGraph.API.Entities;

namespace HolyGraph.API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
