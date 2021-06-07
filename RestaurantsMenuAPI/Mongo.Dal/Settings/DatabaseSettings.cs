namespace Mongo.Dal.Settings
{
    /// <summary>
    /// The database settings.
    /// </summary>
    public class DatabaseSettings : IDatabaseSettings
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
