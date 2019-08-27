using System.Linq;
using Suilder.Reflection.Builder;

namespace Suilder.Test.Reflection
{
    public static class TestExtensions
    {
        public static ITableInfo GetConfig<T>(this ITableBuilder tableBuilder)
        {
            return tableBuilder.GetConfig().Where(x => x.Type == typeof(T)).FirstOrDefault();
        }
    }
}