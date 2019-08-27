using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.OracleDB
{
    public class EngineTest
    {
        [Fact]
        public void Engine_Name()
        {
            IEngine engine = new OracleDBEngine();

            Assert.Equal(EngineName.OracleDB, engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new OracleDBEngine();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }
    }
}