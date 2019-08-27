using System.Collections.Generic;
using System.Linq;

namespace Suilder.Reflection.Builder.Processors
{
    /// <summary>
    /// The base configuration processor.
    /// </summary>
    public abstract class BaseConfigProcessor : IConfigProcessor
    {
        /// <summary>
        /// The configuration data.
        /// </summary>
        /// <value>The configuration data.</value>
        protected ConfigData ConfigData { get; set; }

        /// <summary>
        /// The result of the configuration.
        /// </summary>
        /// <value></value>
        protected ResultData ResultData { get; set; }

        /// <summary>
        /// Process the configuration.
        /// </summary>
        /// <param name="configData">The configuration data.</param>
        /// <param name="resultData">The result of the configuration.</param>
        public void Process(ConfigData configData, ResultData resultData)
        {
            ConfigData = configData;
            ResultData = resultData;

            ProcessData();
        }

        /// <summary>
        /// Process the configuration.
        /// <para>This method is called from the <see cref="Process"/> method after the class scope variables are
        /// initialized.</para>
        /// </summary>
        protected abstract void ProcessData();

        /// <summary>
        /// Gets the configuration of registered types grouped by inheritance level.
        /// </summary>
        /// <param name="configTypes">The configuration of registered types.</param>
        /// <returns>The configuration of registered types grouped by inheritance level.</returns>
        protected IGrouping<int, TableConfig>[] GroupByInheranceLevel(IEnumerable<TableConfig> configTypes)
        {
            return configTypes.GroupBy(x => x.InheritLevel).OrderBy(x => x.First().InheritLevel).ToArray();
        }
    }
}