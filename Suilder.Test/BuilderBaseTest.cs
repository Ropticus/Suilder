using Suilder.Builder;
using Suilder.Engines;
using Suilder.Reflection.Builder;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test
{
    [Collection("SqlBuilder")]
    public abstract class BuilderBaseTest
    {
        protected IEngine engine;

        protected ISqlBuilder sql;

        public BuilderBaseTest()
        {
            sql = SqlBuilder.Instance;

            engine = GetEngine(GetTableBuilder());
        }

        public virtual ITableBuilder GetTableBuilder()
        {
            ITableBuilder tableBuilder = new TableBuilder();
            tableBuilder.Add<Person>();
            tableBuilder.Add<Department>();

            return tableBuilder;
        }

        public virtual IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new Engine(tableBuilder);
        }
    }
}