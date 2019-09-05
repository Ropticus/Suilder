using Suilder.Engines;
using Suilder.Reflection;

namespace Suilder.Test.Engines.SQLiteTest
{
    public abstract class SQLiteBaseTest : BaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new SQLite(tableBuilder);
        }
    }
}