using System.Linq;
using Suilder.Reflection;

namespace Suilder.Test.Reflection
{
    public abstract class BaseTest
    {
        protected TableBuilder tableBuilder;

        public BaseTest()
        {
            tableBuilder = new TableBuilder();
            Configure();
        }

        protected virtual void Configure()
        {
        }

        protected TableInfo GetConfig<T>()
        {
            return tableBuilder.GetConfig().Where(x => x.Type == typeof(T)).First();
        }
    }
}