using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The product.
    /// </summary>
    [CollectionName("Products")]
    public class Product : MongoModelBase
    {
        /// <summary>
        /// Gets or sets the default title.
        /// </summary>
        public string DefaultTitle { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public double Price { get; set; }
    }
}
