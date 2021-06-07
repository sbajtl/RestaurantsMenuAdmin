using Mongo.Dal.Models;
using System.Collections.Generic;

namespace Mongo.Dal.Services.Models
{
    /// <summary>
    /// The category model save.
    /// </summary>
    public class CategoryModelSave
    {
        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public List<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public RestaurantCategory Parent { get; set; }
    }
}
