using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.PostgreSQL
{
    public class EngineTest
    {
        [Fact]
        public void Engine_Name()
        {
            IEngine engine = new PostgreSQLEngine();

            Assert.Equal(EngineName.PostgreSQL, engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new PostgreSQLEngine();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }
    }
}