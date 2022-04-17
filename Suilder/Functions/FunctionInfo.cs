namespace Suilder.Functions
{
    /// <summary>
    /// Implementation of <see cref="IFunctionInfo"/>.
    /// </summary>
    public class FunctionInfo : IFunctionInfo
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