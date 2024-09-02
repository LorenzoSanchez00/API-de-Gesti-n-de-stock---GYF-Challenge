using APIGestionDeStock.Data;
using APIGestionDeStock.Models;
using APIGestionDeStock.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIGestionDeStock.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
    {
        protected DbSet<TEntity> set;
        protected DbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            set = dbContext.Set<TEntity>();
            _dbContext = dbContext;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await set.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            var FoundId = await set.FindAsync(id);

            if (FoundId is null) throw new Exception("No results found");

            return FoundId;
        }

        public async Task<TEntity> Add(TEntity model)
        {
            set.Add(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<TEntity> Delete(int id)
        {
            var recordDb = await set.FindAsync(id);

            if (recordDb is null) throw new Exception("No results found");

            await set.Where(x => x.Id == id).ExecuteDeleteAsync();

            return recordDb;
        }


        public async Task<TEntity> Update(int id, TEntity model)
        {
            var recordDb = await set.FindAsync(id);

            if (recordDb is null) throw new Exception("No results found");

            set.Update(model);
            model.Id = id;

            await _dbContext.SaveChangesAsync();

            return model;
        }

    }
}
