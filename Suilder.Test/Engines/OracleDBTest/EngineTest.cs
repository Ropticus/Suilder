using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.OracleDBTest
{
    public class EngineTest
    {
        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new OracleDB();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }
    }
}