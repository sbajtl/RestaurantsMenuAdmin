using Mongo.Dal.Models;
using Mongo.Dal.Models.Extenders;
using Mongo.Dal.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mongo.Dal.Services
{
    /// <summary>
    /// The restaurant repository.
    /// </summary>
    public interface IRestaurantRepository
    {
        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task InsertOrUpdate(ProductModelSave model);

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task InsertOrUpdate(CategorySave model);


        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task InsertOrUpdate(RestaurantModelSave model);

        /// <summary>
        /// Inserts the.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task InsertCategory(CategoryModelSave model);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task UpdateCategory(CategoryModelUpdate model);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task DeleteCategory(CategoryModelDelete model);

        /// <summary>
        /// Gets the.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<IEnumerable<Restaurant>> Get();

        /// <summary>
        /// Gets the by language async.
        /// </summary>
        /// <param name="languageId">The language.</param>
        /// <returns>A Task.</returns>
        Task<IEnumerable<Restaurant>> GetByLanguageAsync(string languageId);

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<IEnumerable<Language>> GetLanguages();

        /// <summary>
        /// Gets the all categories.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <returns>A Task.</returns>
        Task<IEnumerable<CategoryGet>> GetAllCategories(string languageId);

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="languageId">The language.</param>
        /// <param name="restaurantId">The restaurant id.</param>
        /// <returns>A Task.</returns>
        Task<IEnumerable<Category>> GetCategories(string languageId, string restaurantId);

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task DeleteProduct(ProductModelDelete model);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        Task DeleteCategory(CategoryDelete model);
    }
}
