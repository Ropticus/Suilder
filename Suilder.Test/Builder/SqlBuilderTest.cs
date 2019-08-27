using System;
using Suilder.Builder;
using Xunit;

namespace Suilder.Test.Builder
{
    public class SqlBuilderTest : BuilderBaseTest
    {
        [Fact]
        public void Register_Exception()
        {
            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlBuilder.Register(new SqlBuilder()));
            Assert.Equal("There is already another builder registered.", ex.Message);
        }
    }
}