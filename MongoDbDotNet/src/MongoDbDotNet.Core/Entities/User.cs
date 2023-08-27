using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDbDotNet.Core.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("role")]
    public string Role { get; set; }
}