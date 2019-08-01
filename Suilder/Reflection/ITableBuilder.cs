using System.Collections.Generic;

namespace Suilder.Reflection
{
    /// <summary>
    /// A builder to retrieve the configuration of the tables.
    /// </summary>
    public interface ITableBuilder
    {
        /// <summary>
        /// Gets the config of all tables.
        /// </summary>
        /// <returns>The config of all tables.</returns>
        IList<TableInfo> GetConfig();
    }
}