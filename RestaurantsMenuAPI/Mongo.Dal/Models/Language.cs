namespace Mongo.Dal.Models
{
    /// <summary>
    /// The language.
    /// </summary>
    [CollectionName("Languages")]
    public class Language : MongoModelBase
    {
        /// <summary>
        /// Gets or sets the mnemonic.
        /// </summary>
        public string Mnemonic { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}
