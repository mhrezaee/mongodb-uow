using MongoDB.Driver;
using MongoDbDotNet.Core.Abstracts.Repositories;
using MongoDbDotNet.Core.Entities;

namespace MongoDbDotNet.Infrastructure.Concretes.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    //change to your real collection name in mongo db instead of users
    public UserRepository(IMongoDatabase database) : base(database, "users")
    {
    }

    public async Task<User> GetUserByEmailAsync(string emailAddress)
    {
        var filter = Builders<User>.Filter.Eq(nameof(User.Email), emailAddress);
        return await GetSingleAsync(filter);
    }

    //implement your needed queries.


}