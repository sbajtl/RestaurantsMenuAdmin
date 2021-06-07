using Mongo.Dal.Models;
using System.Collections.Generic;

namespace Mongo.Dal.Services.Models
{
    /// <summary>
    /// The restaurant model save.
    /// </summary>
    public class RestaurantModelSave
    {
        /// <summary>
        /// Gets or sets the restaurants.
        /// </summary>
        public List<Restaurant> Restaurants { get; set; }
    };
}
