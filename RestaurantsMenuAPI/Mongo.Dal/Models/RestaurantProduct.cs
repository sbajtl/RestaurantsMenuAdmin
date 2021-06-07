using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The product.
    /// </summary>
    public class RestaurantProduct
    {
        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ProductId")]
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the unit of measure.
        /// </summary>
        public string UnitOfMeasure { get; set; }
    }
}
