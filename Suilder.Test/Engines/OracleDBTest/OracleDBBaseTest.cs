using Suilder.Engines;
using Suilder.Reflection;

namespace Suilder.Test.Engines.OracleDBTest
{
    public abstract class OracleDBBaseTest : BaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new OracleDB(tableBuilder);
        }
    }
}