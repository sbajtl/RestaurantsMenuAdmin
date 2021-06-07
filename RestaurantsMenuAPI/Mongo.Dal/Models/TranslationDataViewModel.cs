using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Dal.Models
{
    /// <summary>
    /// The translation data view model.
    /// </summary>
    public class TranslationDataViewModel
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public string[] Items { get; set; }

        /// <summary>
        /// Gets or sets the translation.
        /// </summary>
        public CategoryTranslation Translation { get; set; }
    }
}
