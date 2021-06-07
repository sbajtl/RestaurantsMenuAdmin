using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Text.Json.Serialization;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The mongo model base.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class MongoModelBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        //[JsonIgnore]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}