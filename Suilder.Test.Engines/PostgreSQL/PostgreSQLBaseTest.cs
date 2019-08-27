using Suilder.Engines;
using Suilder.Reflection.Builder;

namespace Suilder.Test.Engines.PostgreSQL
{
    public abstract class PostgreSQLBaseTest : BuilderBaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new PostgreSQLEngine(tableBuilder);
        }
    }
}