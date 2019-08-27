namespace Suilder.Reflection.Builder.Processors
{
    /// <summary>
    /// A configuration processor.
    /// </summary>
    public interface IConfigProcessor
    {
        /// <summary>
        /// Process the configuration.
        /// </summary>
        /// <param name="configData">The configuration data.</param>
        /// <param name="resultData">The result of the configuration.</param>
        void Process(ConfigData configData, ResultData resultData);
    }
}