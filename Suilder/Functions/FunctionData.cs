namespace Suilder.Functions
{
    /// <summary>
    /// Implementation of <see cref="IFunctionData"/>.
    /// </summary>
    public class FunctionData : IFunctionData
    {
        /// <summary>
        /// The function name.
        /// </summary>
        /// <value>The function name.</value>
        public string Name { get; set; }

        /// <summary>
        /// A delegate to compile the function.
        /// </summary>
        /// <value>A delegate to compile the function.</value>
        public FunctionCompile Compile { get; set; }
    }
}