using Suilder.Engines;
using Xunit;

namespace Suilder.Test.Engines.SQLServerTest
{
    public class EngineTest
    {
        [Fact]
        public void Escape_Characters()
        {
            IEngine engine = new SQLServer();

            Assert.Equal('[', engine.Options.EscapeStart);
            Assert.Equal(']', engine.Options.EscapeEnd);
        }
    }
}