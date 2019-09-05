using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.SQLiteTest
{
    public class EngineTest
    {
        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new SQLite();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }
    }
}