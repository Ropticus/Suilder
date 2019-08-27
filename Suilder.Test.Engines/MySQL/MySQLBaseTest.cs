using Suilder.Engines;
using Suilder.Reflection.Builder;

namespace Suilder.Test.Engines.MySQL
{
    public abstract class MySQLBaseTest : BuilderBaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new MySQLEngine(tableBuilder);
        }
    }
}