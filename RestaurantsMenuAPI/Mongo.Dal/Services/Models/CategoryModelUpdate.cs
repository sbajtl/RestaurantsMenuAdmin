using Mongo.Dal.Models;

namespace Mongo.Dal.Services.Models
{
    /// <summary>
    /// The category model update.
    /// </summary>
    public class CategoryModelUpdate
    {
        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public RestaurantCategory Category { get; set; }
    }
}
