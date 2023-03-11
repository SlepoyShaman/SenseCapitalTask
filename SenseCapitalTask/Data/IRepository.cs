using SenseCapitalTask.Models;

namespace SenseCapitalTask.Data
{
    public interface IRepository
    {
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        public Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : class, IWithId;
        public Task RemoveByIdAsync<TEntity>(int id) where TEntity : class, IWithId;
        public Task AddAsync<TEntity>(TEntity entity) where TEntity : class;
        public Task UpdateAsync();
    }
}
