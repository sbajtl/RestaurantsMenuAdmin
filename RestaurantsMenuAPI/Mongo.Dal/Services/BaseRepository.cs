using Mongo.Dal.Extensions;
using Mongo.Dal.Models;
using Mongo.Dal.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongo.Dal
{
    /// <summary>
    /// The base service.
    /// </summary>
    public class BaseRepository<TMongoModel>
        where TMongoModel : MongoModelBase, new()
    {
        private readonly IMongoCollection<TMongoModel> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public BaseRepository(IMongoDatabase database)
        {
            collection = database.GetCollection<TMongoModel>();
        }

        /// <summary>
        /// Creates the.
        /// </summary>
        /// <param name="mongoModel">The mongo model.</param>
        /// <returns>A Task.</returns>
        public async Task<string> Create(TMongoModel mongoModel)
        {
            await collection.InsertOneAsync(mongoModel);
            return mongoModel.Id;
        }

        /// <summary>
        /// Gets the.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<IEnumerable<TMongoModel>> Get()
        {
            return await collection.Find(x => true).ToListAsync();
        }

        /// <summary>
        /// Gets the.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns>A Task.</returns>
        public Task<TMongoModel> Get(ObjectId objectId)
        {
            FilterDefinition<TMongoModel> filter = Builders<TMongoModel>.Filter.Eq("Id", objectId);
            return collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
