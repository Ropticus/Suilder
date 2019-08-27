using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.SQLServer
{
    public class EngineTest
    {
        [Fact]
        public void Engine_Name()
        {
            IEngine engine = new SQLServerEngine();

            Assert.Equal(EngineName.SQLServer, engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new SQLServerEngine();

            Assert.Equal('[', engine.Options.EscapeStart);
            Assert.Equal(']', engine.Options.EscapeEnd);
        }
    }
}