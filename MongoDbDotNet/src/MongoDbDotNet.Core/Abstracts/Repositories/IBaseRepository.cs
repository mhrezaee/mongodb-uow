using MongoDB.Driver;

namespace MongoDbDotNet.Core.Abstracts.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(string id);
    Task<TEntity> GetSingleAsync(FilterDefinition<TEntity> predicate);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(string id, TEntity entity);
    Task DeleteAsync(string id);
}