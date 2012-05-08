using MongoDB.Driver;

namespace StackExchange.Profiling.MongoDb.Data
{
    public class ProfiledMongoDatabase : MongoDatabase
    {
        public ProfiledMongoDatabase(MongoServer server, MongoDatabaseSettings settings)
            : base(server, settings)
        {
        }
    }
}
