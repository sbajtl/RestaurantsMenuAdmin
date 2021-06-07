using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The translation.
    /// </summary>
    public class CategoryTranslation
    {
        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("LanguageId")]
        public string LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        [BsonElement("RestaurantCategories")]
        public List<RestaurantCategory> RestaurantCategories { get; set; }
    }
}
