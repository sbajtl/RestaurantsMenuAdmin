using Mongo.Dal.Models.Extenders;
using System.Collections.Generic;

namespace Mongo.Dal.Services.Models
{
    /// <summary>
    /// The data model save.
    /// </summary>
    public class ProductModelSave
    {
        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        public string LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the restaurant id.
        /// </summary>
        public string RestaurantId { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        public List<RestaurantProductGet> Products { get; set; }
    };
}
