using System.Collections.Generic;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The category.
    /// </summary>
    [CollectionName("Categories")]
    public class Category : MongoModelBase
    {
        /// <summary>
        /// Gets or sets the default title.
        /// </summary>
        public string DefaultTitle { get; set; }

        /// <summary>
        /// Gets or sets the translations.
        /// </summary>
        public IEnumerable<Translation> Translations { get; set; }
    }
}
