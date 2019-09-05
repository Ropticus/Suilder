using Suilder.Builder;
using Suilder.Engines;
using Suilder.Functions;
using Suilder.Reflection;
using Suilder.Test.Builder.Tables;

namespace Suilder.Test.Engines
{
    public abstract class BaseTest
    {
        protected IEngine engine;

        protected ISqlBuilder sql;

        public BaseTest()
        {
            TableBuilder tableBuilder = new TableBuilder();
            tableBuilder.Add<Person>();
            tableBuilder.Add<Department>();

            engine = GetEngine(tableBuilder);

            if (SqlBuilder.Instance == null)
            {
                SqlBuilder.Register(new SqlBuilder(), true);
                SqlExp.Initialize();
            }

            sql = SqlBuilder.Instance;
        }

        public virtual IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new Engine(tableBuilder);
        }
    }
}