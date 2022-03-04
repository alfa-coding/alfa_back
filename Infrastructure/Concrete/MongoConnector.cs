using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace alfa_back.Infrastructure.Concrete
{
    public class MongoDbConnector<T> : IConnector<T> where T : IDocument
    {
        private IMongoDatabase database;

        public MongoDbConnector() // Constructor
        {
            var mongoClient = new MongoClient();
            this.database = mongoClient.GetDatabase("myWebApp");

        }

        public T GetElementById(string id, string table)
        {
            var collection = database.GetCollection<T>(table);
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            return collection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<T> GetElements(string table)
        {
            var collection = database.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToEnumerable();


        }

        public bool InsertElement(T record, string table)
        {
            var collection = database.GetCollection<T>(table);
            try
            {
                collection.InsertOne(record);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RemoveElement(string id, string table)
        {
            var collection = database.GetCollection<T>(table);
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);

            try
            {
                collection.FindOneAndDelete(filter);
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public bool Update(string id, T element, string table)
        {
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, element.Id);
            try
            {
                collection.FindOneAndReplace(filter, element);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}