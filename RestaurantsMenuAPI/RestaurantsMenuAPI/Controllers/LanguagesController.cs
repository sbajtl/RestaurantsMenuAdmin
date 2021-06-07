using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mongo.Dal.Models;
using Mongo.Dal.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsMenuAPI.Controllers
{
    /// <summary>
    /// The languages controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> logger;
        private readonly IRestaurantRepository restaurantRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguagesController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="restaurantRepository">The restaurant repository.</param>
        public LanguagesController(ILogger<RestaurantsController> logger, IRestaurantRepository restaurantRepository)
        {
            this.logger = logger;
            this.restaurantRepository = restaurantRepository;
        }

        // GET: api/<LanguagesController>
        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetAsync()
        {
            IEnumerable<Language> languages = await restaurantRepository.GetLanguages();

            if (languages == null)
            {
                return NotFound();
            }

            return languages.ToList();
        }
    }
}
