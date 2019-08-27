using Suilder.Engines;
using Suilder.Reflection.Builder;

namespace Suilder.Test.Engines.SQLServer
{
    public abstract class SQLServerBaseTest : BuilderBaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new SQLServerEngine(tableBuilder);
        }
    }
}