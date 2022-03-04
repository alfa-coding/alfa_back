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
            var connectionString = settings.ComposeConnectionString();
            var database = new MongoClient(connectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public Task<T> GetElementById(string id)
        {

            
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            return Task.Run(() =>
                    {
                        return _collection.Find(filter).SingleOrDefaultAsync();
                    });
        }

        public IQueryable<T> GetElements()
        {
            return _collection.AsQueryable();
        }

        public Task InsertElementAsync(T record)
        {
            try
            {
                return Task.Run(() =>
                    {
                        return _collection.InsertOneAsync(record);
                    });

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task DeleteElementByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public async Task UpdateElementAsync(string id, T element)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, element.Id);
            try
            {
                await _collection.FindOneAndReplaceAsync(filter, element);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}