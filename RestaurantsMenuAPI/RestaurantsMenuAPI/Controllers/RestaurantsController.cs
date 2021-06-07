using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mongo.Dal.Models;
using Mongo.Dal.Models.Extenders;
using Mongo.Dal.Services;
using Mongo.Dal.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsMenuAPI.Controllers
{
    /// <summary>
    /// The restaurants controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> logger;

        private readonly IRestaurantRepository restaurantRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantsController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantRepository restaurantRepository)
        {
            this.logger = logger;
            this.restaurantRepository = restaurantRepository;
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetAsync()
        {
            IEnumerable<Restaurant> restaurants = await restaurantRepository.Get();

            if (restaurants == null)
            {
                return NotFound();
            }

            return restaurants.ToList();
        }

        // GET api/<LanguagesController>/5
        /// <summary>
        /// Gets the by language async.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns>A Task.</returns>
        [HttpGet("{language}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetByLanguageAsync(string language)
        {
            IEnumerable<Restaurant> restaurants = await restaurantRepository.GetByLanguageAsync(language);

            if (restaurants == null)
            {
                return NotFound();
            }

            return restaurants.ToList();
        }

        /// <summary>
        /// Gets the languages async.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpGet]
        [Route("[controller]/Languages")]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguagesAsync()
        {
            IEnumerable<Language> languages = await restaurantRepository.GetLanguages();

            if (languages == null)
            {
                return NotFound();
            }

            return languages.ToList();
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpPost]
        public async Task<ActionResult<List<ProductModelSave>>> PostAsync(ProductModelSave model)
        {
            try
            {
                await restaurantRepository.InsertOrUpdate(model);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>A Task.</returns>
        [HttpPost("Save")]
        public async Task<ActionResult<List<RestaurantModelSave>>> SaveAsync(RestaurantModelSave model)
        {
            try
            {
                await restaurantRepository.InsertOrUpdate(model);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Products the delete async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        [HttpPost("Product/Delete")]
        public async Task<ActionResult<List<ProductModelDelete>>> ProductDeleteAsync(ProductModelDelete model)
        {
            try
            {
                await restaurantRepository.DeleteProduct(model);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
