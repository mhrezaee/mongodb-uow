using MongoDbDotNet.Core.Entities;

namespace MongoDbDotNet.Core.Abstracts.Repositories;

public interface IChatRepository : IBaseRepository<Chat>
{
    Task<List<Chat>> GetAllByEmailAsync(string email, string version);
    Task<List<Chat>> GetAllChatsAsync(string version);
}