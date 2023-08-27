using MongoDB.Driver;
using MongoDbDotNet.Core.Abstracts.Repositories;
using MongoDbDotNet.Core.Entities;
using System.Text.RegularExpressions;

namespace MongoDbDotNet.Infrastructure.Concretes.Repositories;

public class ChatRepository : BaseRepository<Chat>, IChatRepository
{
    //change to your collection name instead of sample chats
    public ChatRepository(IMongoDatabase database) : base(database, "chats")
    {
    }

    public async Task<List<Chat>> GetAllByEmailAsync(string email, string version)
    {
        var filter = Builders<Chat>.Filter.And(
            Builders<Chat>.Filter.Where(x => Regex.IsMatch(x.Message.User, email, RegexOptions.IgnoreCase)),
            Builders<Chat>.Filter.Eq(nameof(Chat.Version), version));
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task<List<Chat>> GetAllChatsAsync(string version)
    {
        var filter = Builders<Chat>.Filter.Eq(nameof(Chat.Version), version);
        return await Collection.Find(filter).ToListAsync();
    }

    //add your implementations

}