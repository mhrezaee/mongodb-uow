namespace MongoDbDotNet.Core.Dtos;

public record CreateChatRequestModel(string UserEmail, string Topic, string Content);