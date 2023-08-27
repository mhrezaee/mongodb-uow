using MongoDbDotNet.Core.Abstracts.Repositories;

namespace MongoDbDotNet.Core.Abstracts;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IChatRepository Chats { get; }

    //add your real collection repository interfaces here.

}