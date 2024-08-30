using APIGestionDeStock.Models;

namespace APIGestionDeStock.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseModel
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(int id, TEntity entity);
        Task<TEntity> Delete(int id);
    }
}
