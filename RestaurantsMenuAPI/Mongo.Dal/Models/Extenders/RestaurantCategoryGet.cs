using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Dal.Models.Extenders
{
    /// <summary>
    /// The restaurant category get.
    /// </summary>
    public class RestaurantCategoryGet : RestaurantCategory
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}
