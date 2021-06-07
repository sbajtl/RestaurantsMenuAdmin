using Mongo.Dal.Models.Extenders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The restaurant category.
    /// </summary>
    public class RestaurantCategory
    {
        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("CategoryId")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category title.
        /// </summary>
        [BsonIgnoreIfNull]
        public string CategoryTitle { get; set; }

        /// <summary>
        /// Gets or sets the category order.
        /// </summary>
        [BsonIgnoreIfDefault]
        public int CategoryOrder { get; set; }

        /// <summary>
        /// Gets or sets the restaurant categories.
        /// </summary>
        [BsonElement("RestaurantCategories")]
        public List<RestaurantCategory> RestaurantCategories { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        [BsonElement("Products")]
        public List<RestaurantProductGet> Products { get; set; }
    }
}
