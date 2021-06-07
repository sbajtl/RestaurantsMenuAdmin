using System;

namespace Mongo.Dal
{
    /// <summary>
    /// The collection name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class CollectionName : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionName"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CollectionName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Name { get; set; }
    }
}
