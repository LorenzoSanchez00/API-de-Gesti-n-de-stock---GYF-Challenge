using APIGestionDeStock.Models;

namespace APIGestionDeStock.Repository.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<List<Product>> filterByBudget(int budget);
    }
}
