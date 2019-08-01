using Suilder.Builder;
using Suilder.Engines;
using Suilder.Functions;
using Suilder.Reflection;
using Suilder.Test.Builder.Tables;

namespace Suilder.Test.Builder
{
    public abstract class BaseTest
    {
        protected IEngine engine;

        protected ISqlBuilder sql;

        public BaseTest()
        {
            TableBuilder tableBuilder = new TableBuilder()
                .Add<Person>()
                .Add<Department>();

            engine = new Engine(tableBuilder);

            if (SqlBuilder.Instance == null)
            {
                SqlBuilder.Register(new SqlBuilder(), true);
                SqlExp.Initialize();
            }

            sql = SqlBuilder.Instance;
        }
    }
}