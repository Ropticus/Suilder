using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.SQLite
{
    public class EngineTest
    {
        [Fact]
        public void Engine_Name()
        {
            IEngine engine = new SQLiteEngine();

            Assert.Equal(EngineName.SQLite, engine.Options.Name);
        }

        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new SQLiteEngine();

            Assert.Equal('\"', engine.Options.EscapeStart);
            Assert.Equal('\"', engine.Options.EscapeEnd);
        }
    }
}