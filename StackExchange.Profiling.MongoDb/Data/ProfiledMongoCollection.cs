using MongoDB.Driver;

namespace StackExchange.Profiling.MongoDb.Data
{
    public class ProfiledMongoCollection<TDefaultDocument> : MongoCollection<TDefaultDocument>
    {
        public ProfiledMongoCollection(MongoDatabase database, MongoCollectionSettings<TDefaultDocument> settings)
            : base(database, settings)
        {
        }
    }

    public class ProfiledMongoCollection : MongoCollection
    {
        public ProfiledMongoCollection(MongoDatabase database, MongoCollectionSettings settings)
            : base(database, settings)
        {
        }
    }
}

