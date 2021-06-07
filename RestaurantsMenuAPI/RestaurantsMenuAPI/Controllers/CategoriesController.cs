using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mongo.Dal.Models;
using Mongo.Dal.Models.Extenders;
using Mongo.Dal.Services;
using Mongo.Dal.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantsMenuAPI.Controllers
{
    /// <summary>
    /// The categories controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<RestaurantsController> logger;
        private readonly IRestaurantRepository restaurantRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="restaurantRepository">The restaurant repository.</param>
        public CategoriesController(ILogger<RestaurantsController> logger, IRestaurantRepository restaurantRepository)
        {
            this.logger = logger;
            this.restaurantRepository = restaurantRepository;
        }

        /// <summary>
        /// Gets the categories async.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns>A Task.</returns>
        [HttpGet("{languageId}/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync(string languageId, string restaurantId)
        {
            IEnumerable<Category> languages = await restaurantRepository.GetCategories(languageId, restaurantId);

            if (languages == null)
            {
                return NotFound();
            }

            return languages.ToList();
        }

        [HttpGet("{languageId}")]
        public async Task<ActionResult<IEnumerable<CategoryGet>>> GetAllCategoriesAsync(string languageId)
        {
            IEnumerable<CategoryGet> categories = await restaurantRepository.GetAllCategories(languageId);

            if (categories == null)
            {
                return NotFound();
            }

            return categories.ToList();
        }

        /// <summary>
        /// Saves the async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        [HttpPost("Save")]
        public async Task<ActionResult<List<CategoryModelSave>>> SaveAsync(CategoryModelSave model)
        {
            try
            {
                await restaurantRepository.InsertCategory(model);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        [HttpPost("Update")]
        public async Task<ActionResult<List<CategoryModelUpdate>>> UpdateAsync(CategoryModelUpdate model)
        {
            try
            {
                await restaurantRepository.UpdateCategory(model);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the async.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        [HttpPost("Delete")]
        public async Task<ActionResult<List<CategoryModelDelete>>> DeleteAsync(CategoryModelDelete model)
        {
            try
            {
                await restaurantRepository.DeleteCategory(model);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteOne")]
        public async Task<ActionResult<List<CategoryDelete>>> DeleteCategoryAsync(CategoryDelete model)
        {
            try
            {
                await restaurantRepository.DeleteCategory(model);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<CategorySave>>> PostAsync(CategorySave model)
        {
            try
            {
                await restaurantRepository.InsertOrUpdate(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
