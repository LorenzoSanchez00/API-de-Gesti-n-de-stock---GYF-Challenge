using APIGestionDeStock.DTOs.Request;
using APIGestionDeStock.Models;

namespace APIGestionDeStock.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task Login(User login);
    }
}
