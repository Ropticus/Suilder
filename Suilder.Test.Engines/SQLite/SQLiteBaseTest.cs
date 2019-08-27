using Suilder.Engines;
using Suilder.Reflection.Builder;

namespace Suilder.Test.Engines.SQLite
{
    public abstract class SQLiteBaseTest : BuilderBaseTest
    {
        public override IEngine GetEngine(ITableBuilder tableBuilder)
        {
            return new SQLiteEngine(tableBuilder);
        }
    }
}