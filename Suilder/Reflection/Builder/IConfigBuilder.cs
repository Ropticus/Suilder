using System.Collections.Generic;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// A builder to obtain the configuration of the tables.
    /// </summary>
    public interface IConfigBuilder
    {
        /// <summary>
        /// Gets the configuration of all tables.
        /// </summary>
        /// <returns>The configuration of all tables.</returns>
        IList<ITableInfo> GetConfig();
    }
}