using Suilder.Engines;
using Suilder.Reflection;

namespace Suilder.Test.Engines.PostgreSQLTest
{
    public abstract class PostgreSQLBaseTest : BaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new PostgreSQL(tableBuilder);
        }
    }
}