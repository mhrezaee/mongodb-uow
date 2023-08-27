
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDbDotNet.Core.Abstracts.Repositories;
using MongoDbDotNet.Core.Abstracts;
using MongoDbDotNet.Infrastructure.Concretes.Repositories;


namespace MongoDbDotNet.Infrastructure.Concretes;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoDatabase _database;

    public UnitOfWork(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("Default"));
        _database = client.GetDatabase(configuration.GetValue<string>("MongoOptions:DatabaseName"));

    }

    private IUserRepository _userRepository;
    public IUserRepository Users => _userRepository ??= new UserRepository(_database);

    private IChatRepository _chatRepository;
    public IChatRepository Chats => _chatRepository ??= new ChatRepository(_database);

}