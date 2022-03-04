using MongoDB.Bson;

namespace alfa_back.Infrastructure.Concrete
{
    public class Document:IDocument
    {
       public ObjectId Id { get; set; }
    }
}