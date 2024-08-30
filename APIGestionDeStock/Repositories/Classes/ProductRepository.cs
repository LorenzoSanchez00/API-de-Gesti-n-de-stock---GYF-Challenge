using APIGestionDeStock.Data;
using Microsoft.EntityFrameworkCore;
using APIGestionDeStock.Models;
using APIGestionDeStock.Repository.Interfaces;
using APIGestionDeStock.Models.ProductCategory;

namespace APIGestionDeStock.Repositories.Classes
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly AppDbContext _context;
		public ProductRepository(AppDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

        public async Task<List<Product>> filterByBudget(int budget)
        {
            var productsByCategory = await _context.Products
                .GroupBy(p => p.Category)
                .ToDictionaryAsync(
                    group => group.Key,
                    group => group.OrderByDescending(p => p.Price).ToList()
                );

            var selectedProducts = new List<Product>();

            if (productsByCategory.TryGetValue(Category.Cat1, out var cat1Products))
            {
                var cat1Product = cat1Products.FirstOrDefault(p => p.Price <= budget);
                if (cat1Product != null)
                {
                    selectedProducts.Add(cat1Product);
                    budget -= (int)cat1Product.Price;
                }
            }

            if (productsByCategory.TryGetValue(Category.Cat2, out var cat2Products))
            {
                var cat2Product = cat2Products.FirstOrDefault(p => p.Price <= budget);
                if (cat2Product != null)
                {
                    selectedProducts.Add(cat2Product);
                }
            }

            if (!selectedProducts.Any() ||
                (productsByCategory.ContainsKey(Category.Cat1) && !selectedProducts.Any(p => p.Category == Category.Cat1)) ||
                (productsByCategory.ContainsKey(Category.Cat2) && !selectedProducts.Any(p => p.Category == Category.Cat2)))
            {
                throw new InvalidOperationException("No products were found that met these conditions");
            }

            return selectedProducts;
        }

    }
}
