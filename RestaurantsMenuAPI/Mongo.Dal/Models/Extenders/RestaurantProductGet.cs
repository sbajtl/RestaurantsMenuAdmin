using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Dal.Models.Extenders
{
    /// <summary>
    /// The restaurant product get.
    /// </summary>
    public class RestaurantProductGet : RestaurantProduct
    {
        /// <summary>
        /// Gets or sets the default title.
        /// </summary>
        public string DefaultTitle { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        [BsonIgnoreIfDefault]
        public double Price { get; set; }
    }
}
