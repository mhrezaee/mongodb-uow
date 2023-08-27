using MongoDbDotNet.Core.Abstracts.Repositories;

namespace MongoDbDotNet.Infrastructure.Concretes.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;

    public BaseRepository(IMongoDatabase database, string collectionName)
    {
        Collection = database.GetCollection<TEntity>(collectionName);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Collection.Find(_ => true).ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(string id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<TEntity> GetSingleAsync(FilterDefinition<TEntity> predicate)
    {
        return await Collection.Find(predicate).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
        await Collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
        await Collection.DeleteOneAsync(filter);
    }
}