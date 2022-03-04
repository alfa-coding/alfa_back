using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace alfa_back.Infrastructure.Concrete
{
    public class MongoDbConnector<T> : IConnector<T> where T : IDocument
    {
        private IMongoCollection<T> _collection;

        public MongoDbConnector(IMongoDbSettings settings) // Constructor
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public T GetElementById(string id)
        {

            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<T> GetElements()
        {
            return _collection.Find(new BsonDocument()).ToEnumerable();


        }

        public bool InsertElement(T record)
        {
            try
            {
                _collection.InsertOne(record);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool RemoveElement(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);

            try
            {
                _collection.FindOneAndDelete(filter);
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public bool Update(string id, T element)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, element.Id);
            try
            {
                _collection.FindOneAndReplace(filter, element);
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}