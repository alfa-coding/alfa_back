using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace alfa_back.Infrastructure
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
        
    }
}