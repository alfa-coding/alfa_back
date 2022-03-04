namespace alfa_back.Infrastructure.Concrete
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Host { get; set; }

        public string ComposeConnectionString();
    }

    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Host { get; set; }

        public string ComposeConnectionString()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return $@"mongodb://{Host}:{Port}"; ;
            }
            return $@"mongodb://{UserName}:{Password}@{Host}:{Port}"; 
        }
    }
}
// $@"mongodb://admin:dimasito@localhost:27017"; 
