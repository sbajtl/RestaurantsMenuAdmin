namespace Mongo.Dal.Models.Extenders
{
    /// <summary>
    /// The category get.
    /// </summary>
    public class CategoryGet : Category
    {
        /// <summary>
        /// Gets or sets the category title.
        /// </summary>
        public string CategoryTitle { get; set; }

        /// <summary>
        /// Gets or sets the category description.
        /// </summary>
        public string CategoryDescription { get; set; }
    }
}
