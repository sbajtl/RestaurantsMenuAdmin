using Mongo.Dal.Extensions;
using Mongo.Dal.Models;
using Mongo.Dal.Models.Extenders;
using Mongo.Dal.Services.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mongo.Dal.Services
{
    /// <summary>
    /// The restaurant repository.
    /// </summary>
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IMongoDatabase database;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantRepository"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public RestaurantRepository(IMongoDatabase database)
        {
            this.database = database;
        }

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task InsertOrUpdate(RestaurantModelSave model)
        {
            var restaurantCollection = this.database.GetCollection<Restaurant>();
            var languages = await GetLanguages();

            foreach (Restaurant restaurant in model.Restaurants)
            {
                if (restaurant.Id == string.Empty)
                {
                    await InsertRestaurant(restaurantCollection, languages, restaurant);
                }
                else
                {
                    await UpdateRestaurant(restaurantCollection, restaurant);

                }
            }
        }

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task InsertOrUpdate(CategorySave model)
        {
            var categoryCollection = this.database.GetCollection<Category>();
            var languages = await GetLanguages();

            foreach (CategoryGet category in model.Categories)
            {
                if (category.Id == string.Empty)
                {
                    await InsertCategory(categoryCollection, languages, category);
                }
                else
                {
                    await UpdateCategory(categoryCollection, category, model.LanguageId);
                }
            }
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="categoryCollection">The category collection.</param>
        /// <param name="category">The category.</param>
        /// <param name="languageId">The language id.</param>
        /// <returns>A Task.</returns>
        private async Task UpdateCategory(IMongoCollection<Category> categoryCollection, CategoryGet category, string languageId)
        {
            Translation translation = category.Translations.FirstOrDefault(x=>x.LanguageId == languageId);
            translation.Title = category.CategoryTitle;
            translation.Description = category.CategoryDescription;

            Category categoryToUpdate = new()
            {
                Id = category.Id,
                DefaultTitle = category.DefaultTitle,
                Translations = category.Translations,
            };

            var tr = categoryToUpdate.Translations.FirstOrDefault(x => x.LanguageId == languageId);
            tr = translation;

            await categoryCollection.ReplaceOneAsync(x => x.Id == category.Id, categoryToUpdate);
        }

        /// <summary>
        /// Inserts the category.
        /// </summary>
        /// <param name="categoryCollection">The category collection.</param>
        /// <param name="languages">The languages.</param>
        /// <param name="category">The category.</param>
        /// <returns>A Task.</returns>
        private async Task InsertCategory(IMongoCollection<Category> categoryCollection, IEnumerable<Language> languages, CategoryGet category)
        {
            var translations = new List<Translation>();
            foreach (Language language in languages)
            {
                translations.Add(new Translation
                {
                    LanguageId = language.Id,
                    Title = category.CategoryTitle,
                    Description = category.CategoryDescription,
                }); ;
            }

            var cat = new Category
            {
                DefaultTitle = category.DefaultTitle,
                Translations = translations,
            };

            await categoryCollection.InsertOneAsync(cat);
        }

        /// <summary>
        /// Inserts the restaurant.
        /// </summary>
        /// <param name="restaurantCollection">The restaurant collection.</param>
        /// <param name="languages">The languages.</param>
        /// <param name="restaurant">The restaurant.</param>
        /// <returns>A Task.</returns>
        private static async Task InsertRestaurant(IMongoCollection<Restaurant> restaurantCollection, IEnumerable<Language> languages, Restaurant restaurant)
        {
            var translations = new List<CategoryTranslation>();
            foreach (Language language in languages)
            {
                translations.Add(new CategoryTranslation
                {
                    LanguageId = language.Id,
                    RestaurantCategories = new List<RestaurantCategory>()
                }); ;
            }

            var rest = new Restaurant
            {
                Title = restaurant.Title,
                Description = restaurant.Description,
                StyleCode = restaurant.StyleCode,
                Translations = translations,
            };

            await restaurantCollection.InsertOneAsync(rest);
        }

        /// <summary>
        /// Updates the restaurant.
        /// </summary>
        /// <param name="restaurantCollection">The restaurant collection.</param>
        /// <param name="restaurant">The restaurant.</param>
        /// <returns>A Task.</returns>
        private static async Task UpdateRestaurant(IMongoCollection<Restaurant> restaurantCollection, Restaurant restaurant)
        {
            FilterDefinition<Restaurant> filterDefinition;
            UpdateDefinition<Restaurant> updateDefinition;

            filterDefinition =
                Builders<Restaurant>.Filter.Eq(x => x.Id, restaurant.Id);

            updateDefinition = Builders<Restaurant>.Update
            .Set(x => x.Title, restaurant.Title)
            .Set(x => x.Description, restaurant.Description)
            .Set(x => x.StyleCode, restaurant.StyleCode);

            await restaurantCollection.FindOneAndUpdateAsync(filterDefinition, updateDefinition);
        }

        /// <summary>
        /// Inserts the or update.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task InsertOrUpdate(ProductModelSave model)
        {
            var restaurantCollection = this.database.GetCollection<Restaurant>();
            var productCollection = this.database.GetCollection<Product>();

            var restaurantsFilter =
                Builders<Restaurant>.Filter.Eq(x => x.Id, model.RestaurantId) &
                Builders<Restaurant>.Filter.Eq("Translations.LanguageId", model.LanguageId);

            var restaurant = await restaurantCollection.Find(restaurantsFilter).FirstOrDefaultAsync();

            foreach (RestaurantProductGet product in model.Products)
            {
                //SetFiltersAndDefinitions(model, product, out List<ArrayFilterDefinition> arrayFilters, out FilterDefinition<Restaurant> filter, out UpdateDefinition<Restaurant> update);

                if (product.ProductId == string.Empty)
                {
                    await InsertProduct(productCollection, product);
                }
                else
                {
                    await UpdateProduct(productCollection, product);

                    //ArrayFilterDefinition<RestaurantProduct> productFilter = new BsonDocument("p.ProductId", new BsonDocument("$eq", ObjectId.Parse(product.ProductId)));
                    //arrayFilters.Add(productFilter);
                }
                var categories = restaurant.Translations.FirstOrDefault(x => x.LanguageId == model.LanguageId).RestaurantCategories;
                SaveProductsForCategory(categories, model, product);
                await restaurantCollection.ReplaceOneAsync(x => x.Id == model.RestaurantId, restaurant);
                //UpdateOptions updateOptions = new() { ArrayFilters = arrayFilters };

                //UpdateResult updated = await restaurantCollection.UpdateOneAsync(filter, update, updateOptions);
            }
        }

        /// <summary>
        /// Saves the products for category.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="model">The model.</param>
        /// <param name="product">The product.</param>
        private void SaveProductsForCategory(List<RestaurantCategory> data, ProductModelSave model, RestaurantProductGet product)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].CategoryId == model.CategoryId)
                {
                    RestaurantProductGet pr = data[i].Products.FirstOrDefault(x => x.ProductId == product.ProductId);

                    if (pr == null)
                    {
                        data[i].Products.Add(product);
                    }
                    else
                    {
                        var existing = data[i].Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                        existing.Title = product.Title;
                        existing.DefaultTitle = product.DefaultTitle;
                        existing.Description = product.Description;
                        existing.UnitOfMeasure = product.UnitOfMeasure;
                    }

                    break;
                }
                else if (data[i].RestaurantCategories != null && data[i].RestaurantCategories!.Count > 0)
                {
                    SaveProductsForCategory(data[i].RestaurantCategories, model, product);
                }
            }
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="productCollection">The product collection.</param>
        /// <param name="product">The product.</param>
        /// <returns>A Task.</returns>
        private static async Task UpdateProduct(IMongoCollection<Product> productCollection, RestaurantProductGet product)
        {
            Product pr = CreateProduct(product);
            FilterDefinition<Product> filterDefinition;
            UpdateDefinition<Product> updateDefinition;

            filterDefinition =
                Builders<Product>.Filter.Eq(x => x.Id, pr.Id);

            updateDefinition = Builders<Product>.Update
            .Set(x => x.DefaultTitle, product.DefaultTitle)
            .Set(x => x.Price, product.Price);

            await productCollection.FindOneAndUpdateAsync(filterDefinition, updateDefinition);
        }

        /// <summary>
        /// Inserts the product.
        /// </summary>
        /// <param name="productCollection">The product collection.</param>
        /// <param name="product">The product.</param>
        /// <returns>A Task.</returns>
        private static async Task InsertProduct(IMongoCollection<Product> productCollection, RestaurantProductGet product)
        {
            product.ProductId = ObjectId.GenerateNewId().ToString();
            Product pr = CreateProduct(product);

            await productCollection.InsertOneAsync(pr);
            product.Price = 0;
        }

        /// <summary>
        /// Sets the filters and definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="product">The product.</param>
        /// <param name="arrayFilters">The array filters.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="update">The update.</param>
        private static void SetFiltersAndDefinitions(ProductModelSave model, RestaurantProductGet product, out List<ArrayFilterDefinition> arrayFilters, out FilterDefinition<Restaurant> filter, out UpdateDefinition<Restaurant> update)
        {
            arrayFilters = SetFilters(model);
            filter = GenerateInsertOrUpdateFilter(model);
            update = GenerateUpdateDefinition(product);
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>A Product.</returns>
        private static Product CreateProduct(RestaurantProductGet product)
        {
            return new()
            {
                Id = product.ProductId,
                DefaultTitle = product.DefaultTitle,
                Price = product.Price,
            };
        }

        /// <summary>
        /// Sets the filters.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A list of ArrayFilterDefinitions.</returns>
        private static List<ArrayFilterDefinition> SetFilters(ProductModelSave model)
        {
            var arrayFilters = new List<ArrayFilterDefinition>();
            ArrayFilterDefinition<CategoryTranslation> translationFilter = new BsonDocument("t.LanguageId", new BsonDocument("$eq", ObjectId.Parse(model.LanguageId)));
            ArrayFilterDefinition<RestaurantCategory> categoryFilter = new BsonDocument("c.CategoryId", new BsonDocument("$eq", ObjectId.Parse(model.CategoryId)));
            arrayFilters.Add(translationFilter);
            arrayFilters.Add(categoryFilter);
            return arrayFilters;
        }

        /// <summary>
        /// Generates the update definition.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>An UpdateDefinition.</returns>
        private static UpdateDefinition<Restaurant> GenerateUpdateDefinition(RestaurantProductGet product)
        {
            UpdateDefinition<Restaurant> updateDefinition;
            if (product.ProductId == string.Empty)
            {
                updateDefinition = Builders<Restaurant>.Update.Push("Translations.$[t].RestaurantCategories.$[c].Products", product);
            }
            else
            {
                updateDefinition = Builders<Restaurant>.Update
                    .Set("Translations.$[t].RestaurantCategories.$[c].Products.$[p].Title", product.Title)
                    .Set("Translations.$[t].RestaurantCategories.$[c].Products.$[p].Description", product.Description)
                    .Set("Translations.$[t].RestaurantCategories.$[c].Products.$[p].UnitOfMeasure", product.UnitOfMeasure);
            }
            return updateDefinition;
        }

        /// <summary>
        /// Generates the insert or update filter.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A FilterDefinition.</returns>
        private static FilterDefinition<Restaurant> GenerateInsertOrUpdateFilter(ProductModelSave model)
        {
            return Builders<Restaurant>.Filter.Eq(x => x.Id, model.RestaurantId) &
                Builders<Restaurant>.Filter.Eq("Translations.LanguageId", model.LanguageId) &
                Builders<Restaurant>.Filter.Eq("Translations.RestaurantCategories.CategoryId", model.CategoryId);
        }

        /// <summary>
        /// Gets the.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<IEnumerable<Restaurant>> Get()
        {
            var collection = database.GetCollection<Restaurant>();
            return await collection.Find(x => true).ToListAsync();
        }

        /// <summary>
        /// Gets the by language async.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <returns>A Task.</returns>
        public async Task<IEnumerable<Restaurant>> GetByLanguageAsync(string languageId)
        {
            var collection = database.GetCollection<Restaurant>();

            var filter = Builders<Restaurant>.Filter.Empty; //Builders<Restaurant>.Filter.Eq("Translations.LanguageId", languageId);
            var data = await collection.Find(filter).ToListAsync();
            var dataToReturn = CreateDataToReturn(data.OrderByDescending(x => x.Id).ToList(), languageId);

            return dataToReturn;
        }

        /// <summary>
        /// Creates the data to return.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>A list of Restaurants.</returns>
        private List<Restaurant> CreateDataToReturn(List<Restaurant> data, string languageId)
        {
            var dataToReturn = data.SelectMany(r => new List<Restaurant>
            {
                new Restaurant
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    StyleCode = r.StyleCode,
                    Translations = r.Translations.SelectMany(t => new List<CategoryTranslation>
                    {
                       new CategoryTranslation
                       {
                           LanguageId = t.LanguageId,
                           RestaurantCategories = Categories(languageId, t).OrderBy(x => x.CategoryOrder).ToList()
                       }

                    }).OrderByDescending(x => x.LanguageId).ToList(),
                }
            }).ToList();

            return dataToReturn;
        }

        /// <summary>
        /// Categories the.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <param name="t">The t.</param>
        /// <returns>A list of RestaurantCategories.</returns>
        private IEnumerable<RestaurantCategory> Categories(string languageId, CategoryTranslation t)
        {
            CreateCategoryRecursive(t.RestaurantCategories, languageId);

            return t.RestaurantCategories;
        }

        /// <summary>
        /// Creates the category recursive.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="languageId">The language id.</param>
        public void CreateCategoryRecursive(List<RestaurantCategory> data, string languageId)
        {
            if (data == null || data.Count == 0)
                return;

            for (int i = 0; i < data.Count; i++)
            {
                var newCategory = new RestaurantCategory
                {
                    CategoryId = data[i].CategoryId,
                    CategoryTitle = GetCategoryByIdAndLanguage(data[i].CategoryId).Translations.FirstOrDefault(x => x.LanguageId == languageId).Title,//c.CategoryTitle,
                    CategoryOrder = data[i].CategoryOrder,
                    RestaurantCategories = data[i].RestaurantCategories.OrderBy(x => x.CategoryOrder).ToList(),
                    Products = data[i].Products.SelectMany(
                        p => new List<RestaurantProductGet>
                        {
                            new RestaurantProductGet
                            {
                                ProductId = p.ProductId,
                                DefaultTitle = GetProductById(p.ProductId).DefaultTitle,
                                Title = p.Title,
                                Description = p.Description,
                                UnitOfMeasure = p.UnitOfMeasure,
                                Price = GetProductById(p.ProductId).Price,
                            }
                        }).OrderByDescending(x => x.ProductId).ToList()
                };

                data[i] = newCategory;

                if (newCategory.RestaurantCategories != null && newCategory.RestaurantCategories.Any())
                {
                    CreateCategoryRecursive(newCategory.RestaurantCategories, languageId);
                }
            }

            // Factorial(data, languageId);
        }

        /// <summary>
        /// Gets the category by id and language.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="languageId">The language id.</param>
        /// <returns>A Category.</returns>
        private Category GetCategoryByIdAndLanguage(string categoryId)
        {
            var collection = database.GetCollection<Category>();

            var filter =
                Builders<Category>.Filter.Eq(x => x.Id, categoryId);

            var category = collection.Find(filter).ToList().FirstOrDefault();

            return category;
        }

        /// <summary>
        /// Gets the product by id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>A Product.</returns>
        private Product GetProductById(string productId)
        {
            var collection = database.GetCollection<Product>();
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var product = collection.Find(filter).ToList().FirstOrDefault();
            return product;
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<IEnumerable<Language>> GetLanguages()
        {
            var collection = database.GetCollection<Language>();
            return await collection.Find(x => true).ToListAsync();
        }

        /// <summary>
        /// Gets the all categories.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <returns>A Task.</returns>
        public async Task<IEnumerable<CategoryGet>> GetAllCategories(string languageId)
        {
            var collection = database.GetCollection<Category>();

            var filter = Builders<Category>.Filter.Eq("Translations.LanguageId", languageId);
            var categories = await collection.Find(filter).ToListAsync();
            var dataToReturn = CategoryDataToReturn(categories.OrderByDescending(x => x.Id).ToList(), languageId);
            return dataToReturn;
        }

        /// <summary>
        /// Categories the data to return.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <param name="languageId">The language id.</param>
        /// <returns>A list of CategoryGets.</returns>
        private List<CategoryGet> CategoryDataToReturn(List<Category> categories, string languageId)
        {
            var dataToReturn = categories.SelectMany(c => new List<CategoryGet>
            {
                new CategoryGet
                {
                    Id = c.Id,
                    DefaultTitle = c.DefaultTitle,
                    Translations = c.Translations,
                    CategoryTitle = c.Translations.FirstOrDefault(x => x.LanguageId == languageId).Title,
                    CategoryDescription = c.Translations.FirstOrDefault(x => x.LanguageId == languageId).Description,
                }
            }).ToList();

            return dataToReturn;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<IEnumerable<Category>> GetCategories(string languageId, string restaurantId)
        {
            var collection = database.GetCollection<Category>();
            var restaurantsCollection = database.GetCollection<Restaurant>();

            var restaurantsFilter =
                Builders<Restaurant>.Filter.Eq(x => x.Id, restaurantId) &
                Builders<Restaurant>.Filter.Eq("Translations.LanguageId", languageId);

            var restaurant = await restaurantsCollection.Find(restaurantsFilter).FirstOrDefaultAsync();

            var categories = restaurant.Translations.FirstOrDefault(x => x.LanguageId == languageId).RestaurantCategories;
            List<string> categs = new();

            FillCategoriesToFind(categories, categs);

            var filter =
                Builders<Category>.Filter.Eq("Translations.LanguageId", languageId) &
                !Builders<Category>.Filter.In(x => x.Id, categs.ToArray());

            var data = await collection.Find(filter).ToListAsync();

            return await collection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Fills the categories to find.
        /// </summary>
        /// <param name="categories">The categories.</param>
        /// <param name="categs">The categs.</param>
        private static void FillCategoriesToFind(List<RestaurantCategory> categories, List<string> categs)
        {
            foreach (RestaurantCategory item in categories)
            {
                categs.Add(item.CategoryId);

                if (item.RestaurantCategories != null && item.RestaurantCategories.Any())
                {
                    FillCategoriesToFind(item.RestaurantCategories, categs);
                }
            }
        }

        /// <summary>
        /// Inserts the.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task InsertCategory(CategoryModelSave model)
        {
            var restaurantsCollection = database.GetCollection<Restaurant>();
            var filter = Builders<Restaurant>.Filter.Eq(x => x.Id, model.RestaurantId);

            Restaurant restaurant = await restaurantsCollection.Find(filter).FirstOrDefaultAsync();

            foreach (string categoryId in model.Categories)
            {
                CategoryTranslation translation = restaurant.Translations.FirstOrDefault(); // hr
                //foreach (CategoryTranslation translation in restaurant.Translations)
                //{
                var arrayFilters = new List<ArrayFilterDefinition>();
                ArrayFilterDefinition<BsonDocument> translationFilter = new BsonDocument("t.LanguageId", new BsonDocument("$eq", ObjectId.Parse(translation.LanguageId)));

                arrayFilters.Add(translationFilter);

                RestaurantCategory last = translation.RestaurantCategories.LastOrDefault();

                RestaurantCategory rastaurantCategory = new()
                {
                    CategoryId = categoryId,
                    CategoryOrder = last != null ? translation.RestaurantCategories.LastOrDefault().CategoryOrder + 10 : 10,
                    RestaurantCategories = new List<RestaurantCategory>(),
                    Products = new List<RestaurantProductGet>(),
                };

                if (model.Parent != null)
                {
                    InsertIntoCategories(translation.RestaurantCategories, model, rastaurantCategory);
                    await restaurantsCollection.ReplaceOneAsync(x => x.Id == model.RestaurantId, restaurant);
                }
                else
                {
                    UpdateDefinition<Restaurant> update = Builders<Restaurant>.Update.Push("Translations.$[t].RestaurantCategories", rastaurantCategory);
                    UpdateOptions updateOptions = new() { ArrayFilters = arrayFilters };
                    await restaurantsCollection.UpdateOneAsync(filter, update, updateOptions);
                }
            }
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task UpdateCategory(CategoryModelUpdate model)
        {
            var restaurantsCollection = database.GetCollection<Restaurant>();
            var filter = Builders<Restaurant>.Filter.Eq(x => x.Id, model.RestaurantId);

            Restaurant restaurant = await restaurantsCollection.Find(filter).FirstOrDefaultAsync();

            CategoryTranslation translation = restaurant.Translations.FirstOrDefault();

            UpdateCategoryOrder(translation.RestaurantCategories, model);
            await restaurantsCollection.ReplaceOneAsync(x => x.Id == model.RestaurantId, restaurant);
        }

        /// <summary>
        /// Finds the category by id.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="model">The model.</param>
        /// <param name="rastaurantCategory">The rastaurant category.</param>
        private void InsertIntoCategories(List<RestaurantCategory> data, CategoryModelSave model, RestaurantCategory rastaurantCategory)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].CategoryId == model.Parent.CategoryId)
                {
                    RestaurantCategory last = data[i].RestaurantCategories.LastOrDefault();
                    rastaurantCategory.CategoryOrder = last != null ? last.CategoryOrder + 10 : 10;
                    data[i].RestaurantCategories.Add(rastaurantCategory);
                    break;
                }
                else if (data[i].RestaurantCategories != null && data[i].RestaurantCategories!.Count > 0)
                {
                    InsertIntoCategories(data[i].RestaurantCategories, model, rastaurantCategory);
                }
            }
        }

        /// <summary>
        /// Updates the category order.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="model">The model.</param>
        private void UpdateCategoryOrder(List<RestaurantCategory> data, CategoryModelUpdate model)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].CategoryId == model.Category.CategoryId)
                {
                    data[i].CategoryOrder = model.Category.CategoryOrder;
                    break;
                }
                else if (data[i].RestaurantCategories != null && data[i].RestaurantCategories!.Count > 0)
                {
                    UpdateCategoryOrder(data[i].RestaurantCategories, model);
                }
            }
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task DeleteCategory(CategoryModelDelete model)
        {
            var restaurantsCollection = database.GetCollection<Restaurant>();
            var filter = Builders<Restaurant>.Filter.Eq(x => x.Id, model.RestaurantId);

            Restaurant restaurant = await restaurantsCollection.Find(filter).FirstOrDefaultAsync();

            CategoryTranslation translation = restaurant.Translations.FirstOrDefault();

            DeleteFromCategory(translation.RestaurantCategories, model);
            await restaurantsCollection.ReplaceOneAsync(x => x.Id == model.RestaurantId, restaurant);
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task DeleteCategory(CategoryDelete model)
        {
            var categoryCollection = database.GetCollection<Category>();
            var filter = Builders<Category>.Filter.Eq(x => x.Id, model.Category.Id);

            await categoryCollection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Deletes the from category.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="model">The model.</param>
        private void DeleteFromCategory(List<RestaurantCategory> data, CategoryModelDelete model)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].CategoryId == model.Category.CategoryId)
                {
                    data.RemoveAt(i);
                    break;
                }
                else if (data[i].RestaurantCategories != null && data[i].RestaurantCategories!.Count > 0)
                {
                    DeleteFromCategory(data[i].RestaurantCategories, model);
                }
            }
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        public async Task DeleteProduct(ProductModelDelete model)
        {
            var restaurantsCollection = database.GetCollection<Restaurant>();
            var filter = Builders<Restaurant>.Filter.Eq(x => x.Id, model.RestaurantId);

            Restaurant restaurant = await restaurantsCollection.Find(filter).FirstOrDefaultAsync();

            CategoryTranslation translation = restaurant.Translations.FirstOrDefault();

            DeleteProductFromCategory(translation.RestaurantCategories, model);
            await restaurantsCollection.ReplaceOneAsync(x => x.Id == model.RestaurantId, restaurant);

        }

        /// <summary>
        /// Deletes the product from category.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="model">The model.</param>
        /// <param name="product">The product.</param>
        private void DeleteProductFromCategory(List<RestaurantCategory> data, ProductModelDelete model)
        {
            for (var i = 0; i < data.Count; i++)
            {
                if (data[i].CategoryId == model.CategoryId)
                {
                    RestaurantProductGet pr = data[i].Products.FirstOrDefault(x => x.ProductId == model.Product.ProductId);

                    if (pr != null)
                    {
                        data[i].Products.Remove(pr);
                        var productsCollection = database.GetCollection<Product>();
                        var deleteProductFilter = Builders<Product>.Filter.Eq(x => x.Id, pr.ProductId);
                        productsCollection.DeleteOne(deleteProductFilter);
                    }
                    break;
                }
                else if (data[i].RestaurantCategories != null && data[i].RestaurantCategories!.Count > 0)
                {
                    DeleteProductFromCategory(data[i].RestaurantCategories, model);
                }
            }
        }
    }
}
