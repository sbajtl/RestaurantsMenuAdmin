using Mongo.Dal.Models.Extenders;
using System.Collections.Generic;

namespace Mongo.Dal.Services.Models
{
    /// <summary>
    /// The category save.
    /// </summary>
    public class CategorySave
    {
        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        public string LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public List<CategoryGet> Categories { get; set; }
    }
}
