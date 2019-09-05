using Suilder.Engines;
using Suilder.Reflection;

namespace Suilder.Test.Engines.MySQLTest
{
    public abstract class MySQLBaseTest : BaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new MySQL(tableBuilder);
        }
    }
}