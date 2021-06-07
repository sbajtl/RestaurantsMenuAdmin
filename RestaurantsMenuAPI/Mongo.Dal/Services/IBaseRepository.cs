using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongo.Dal.Services
{
    /// <summary>
    /// The base repository.
    /// </summary>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Creates the.
        /// </summary>
        /// <param name="mongoModel">The mongo model.</param>
        /// <returns>A Task.</returns>
        Task<string> Create(T mongoModel);

        /// <summary>
        /// Gets the.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<IEnumerable<T>> Get();

        /// <summary>
        /// Gets the.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns>A Task.</returns>
        Task<T> Get(ObjectId objectId);
    }
}
