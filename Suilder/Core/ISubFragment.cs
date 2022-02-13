namespace Suilder.Core
{
    /// <summary>
    /// Any <see cref="IQueryFragment"/> that must be enclosed in parentheses to preserve the operator precedence.
    /// </summary>
    public interface ISubFragment : IQueryFragment
    {
    }
}