using Suilder.Engines;
using Suilder.Reflection;

namespace Suilder.Test.Engines.SQLServerTest
{
    public abstract class SQLServerBaseTest : BaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new SQLServer(tableBuilder);
        }
    }
}