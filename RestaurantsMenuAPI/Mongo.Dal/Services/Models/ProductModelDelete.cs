using Mongo.Dal.Models;

namespace Mongo.Dal.Services.Models
{
    /// <summary>
    /// The product model delete.
    /// </summary>
    public class ProductModelDelete
    {
        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public RestaurantProduct Product { get; set; }
    }
}
