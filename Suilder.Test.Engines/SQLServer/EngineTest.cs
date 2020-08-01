using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.SQLServer
{
    public class EngineTest
    {
        protected IEngine engine = new SQLServerEngine();

        [Fact]
        public void Engine_Name()
        {
            Assert.Equal(EngineName.SQLServer, engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            Assert.Equal('[', engine.Options.EscapeStart);
            Assert.Equal(']', engine.Options.EscapeEnd);
        }

        [Fact]
        public void Parameters()
        {
            Assert.Equal("@p", engine.Options.ParameterPrefix);
            Assert.True(engine.Options.ParameterIndex);
        }
    }
}