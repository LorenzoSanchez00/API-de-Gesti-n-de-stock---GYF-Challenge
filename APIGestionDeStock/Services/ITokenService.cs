using APIGestionDeStock.Models;

namespace APIGestionDeStock.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
