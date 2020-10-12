using API.Entities;

namespace API.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}