using Suilder.Builder;
using Suilder.Engines;
using Suilder.Functions;
using Suilder.Performance.Tables;
using Suilder.Reflection.Builder;

namespace Suilder.Performance
{
    public class BaseBenchmark
    {
        protected IEngine engine;

        protected ISqlBuilder sql;

        public BaseBenchmark()
        {
            sql = SqlBuilder.Register(new SqlBuilder());
            SqlExp.Initialize();

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