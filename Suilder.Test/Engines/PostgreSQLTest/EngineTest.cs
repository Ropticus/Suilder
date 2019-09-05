using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.PostgreSQLTest
{
    public class EngineTest
    {
        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new PostgreSQL();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }
    }
}