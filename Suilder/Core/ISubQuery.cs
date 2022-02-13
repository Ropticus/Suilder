namespace Suilder.Core
{
    /// <summary>
    /// Any <see cref="IQueryFragment"/> that must be enclosed in parentheses, like a subquery or a list of values.
    /// </summary>
    public interface ISubQuery : ISubFragment
    {
    }
}