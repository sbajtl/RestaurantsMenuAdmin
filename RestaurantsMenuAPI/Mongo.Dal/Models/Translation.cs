using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The translation.
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("LanguageId")]
        public string LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

    }
}
