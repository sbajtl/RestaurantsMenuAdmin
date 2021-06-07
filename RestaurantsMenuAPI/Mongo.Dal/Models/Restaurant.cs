using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The restaurant.
    /// </summary>
    [CollectionName("Restaurants")]
    public class Restaurant : MongoModelBase
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the style code.
        /// </summary>
        public string StyleCode { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public List<CategoryTranslation> Translations { get; set; }
    }
}
