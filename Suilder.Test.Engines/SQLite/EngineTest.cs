using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.SQLite
{
    public class EngineTest
    {
        protected IEngine engine = new SQLiteEngine();

        [Fact]
        public void Engine_Name()
        {
            Assert.Equal(EngineName.SQLite, engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }

        [Fact]
        public void Parameters()
        {
            Assert.Equal("@p", engine.Options.ParameterPrefix);
            Assert.True(engine.Options.ParameterIndex);
        }
    }
}