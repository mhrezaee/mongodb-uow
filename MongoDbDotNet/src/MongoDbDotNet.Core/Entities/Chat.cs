using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDbDotNet.Core.Entities;

public class Chat
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("message")]
    public Message Message { get; set; }
    
    [BsonElement("version")]
    public string Version { get; set; }
}

public class Message
{
    [BsonElement("topic")]
    public string Topic { get; set; }
    [BsonElement("user")]
    public string User { get; set; }
    [BsonElement("created_date")]
    public string CreatedDate { get; set; }
    [BsonElement("created_timestamp")]
    public double CreatedTimestamp { get; set; }
}