using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Dal.Extensions
{
    /// <summary>
    /// The mongo database extensions.
    /// </summary>
    public static class MongoDatabaseExtensions
    {
        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <typeparam name="TDocument">The type.</typeparam>
        /// <returns>An IMongoCollection.</returns>
        public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase db, MongoCollectionSettings settings = null)
            where TDocument : new()
        {
            var name = MongoDatabaseHelper.GetCollectionName<TDocument>();
            return name != null ? db.GetCollection<TDocument>(name, settings) : null;
        }
    }
}
