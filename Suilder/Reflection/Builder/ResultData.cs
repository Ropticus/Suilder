using System;
using System.Collections.Generic;
using System.Linq;

namespace Suilder.Reflection.Builder
{
    /// <summary>
    /// The result of the configuration.
    /// </summary>
    public class ResultData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBuilder"/> class.
        /// </summary>
        /// <param name="resultTypes">The result types.</param>
        public ResultData(Type[] resultTypes)
        {
            this.resultTypes = resultTypes.ToDictionary(x => x.FullName, x =>
            {
                Type type = typeof(TableInfo<>).MakeGenericType(x);
                return (TableInfo)Activator.CreateInstance(type);
            });
        }

        /// <summary>
        /// The information of registered types.
        /// </summary>
        protected Dictionary<string, TableInfo> resultTypes;

        /// <summary>
        /// The information of registered types.
        /// </summary>
        public IReadOnlyDictionary<string, TableInfo> ResultTypes => resultTypes;

        /// <summary>
        /// Gets the configuration of a type.
        /// </summary>
        /// <param name="type">The type to get the configuration.</param>
        /// <returns>The configuration.</returns>
        public TableInfo GetConfig(Type type)
        {
            ResultTypes.TryGetValue(type.FullName, out TableInfo config);
            return config;
        }

        /// <summary>
        /// Gets the parent configuration of a type.
        /// </summary>
        /// <param name="type">The type to get the parent configuration.</param>
        /// <returns>The configuration.</returns>
        public TableInfo GetParentConfig(Type type)
        {
            ResultTypes.TryGetValue(type.BaseType.FullName, out TableInfo config);
            return config;
        }
    }
}