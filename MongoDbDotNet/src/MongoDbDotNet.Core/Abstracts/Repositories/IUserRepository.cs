using MongoDbDotNet.Core.Entities;

namespace MongoDbDotNet.Core.Abstracts.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetUserByEmailAsync(string emailAddress);
}