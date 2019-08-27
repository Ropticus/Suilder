using Suilder.Engines;
using Suilder.Reflection.Builder;

namespace Suilder.Test.Engines.OracleDB
{
    public abstract class OracleDBBaseTest : BuilderBaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new OracleDBEngine(tableBuilder);
        }
    }
}