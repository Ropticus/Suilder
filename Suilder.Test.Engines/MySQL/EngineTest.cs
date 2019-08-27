using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.MySQL
{
    public class EngineTest
    {
        [Fact]
        public void Engine_Name()
        {
            IEngine engine = new MySQLEngine();

            Assert.Equal(EngineName.MySQL, engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new MySQLEngine();

            Assert.Equal('`', engine.Options.EscapeStart);
            Assert.Equal('`', engine.Options.EscapeEnd);
        }
    }
}