using MongoDB.Driver;

namespace StackExchange.Profiling.MongoDb.Data
{
    public class ProfiledMongoServer : MongoServer
    {
        public ProfiledMongoServer(MongoServerSettings settings)
            : base(settings)
        {
        }
    }
}
