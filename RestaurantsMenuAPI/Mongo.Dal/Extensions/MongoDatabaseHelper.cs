using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Dal.Extensions
{
    /// <summary>
    /// The mongo database helper.
    /// </summary>
    public static class MongoDatabaseHelper
    {
        /// <summary>
        /// Gets the collection name.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>A string.</returns>
        public static string GetCollectionName<T>()
            where T : new()
        {
            T o = new T();
            string collectionName = null;
            var nameAttribute = o.GetType().GetCustomAttributes().ToList().Find(x => x.GetType().Name == nameof(CollectionName));
            if (nameAttribute != null)
            {
                if (nameAttribute != null)
                {
                    var n = nameAttribute.GetType().GetRuntimeProperties().ToList().Find(x => x.Name == nameof(CollectionName.Name));
                    if (n != null)
                    {
                        var tmp = n.GetValue(nameAttribute);
                        return tmp != null ? tmp as string : collectionName;
                    }
                }
            }

            return collectionName;
        }
    }
}
