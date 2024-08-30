using APIGestionDeStock.Data;
using APIGestionDeStock.DTOs.Request;
using APIGestionDeStock.Models;
using APIGestionDeStock.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIGestionDeStock.Repositories.Classes
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByEmail(string user)
        {
            var dbUserEmail = await set.FirstOrDefaultAsync(x => x.Email == user);
            return dbUserEmail!;
        }

        

        public async Task Login(User login)
        {
            var user = await set.FirstOrDefaultAsync(x => x.Email == login.Email && x.Password == login.Password);
        }
    }
}
