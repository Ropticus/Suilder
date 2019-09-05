using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.MySQLTest
{
    public class EngineTest
    {
        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new MySQL();

            Assert.Equal('`', engine.Options.EscapeStart);
            Assert.Equal('`', engine.Options.EscapeEnd);
        }
    }
}