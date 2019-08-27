using System;
using Suilder.Exceptions;
using Xunit;

namespace Suilder.Test.Exceptions
{
    public class ClauseNotSupportedExceptionTest
    {
        [Fact]
        public void Default_Constructor()
        {
            ClauseNotSupportedException ex = new ClauseNotSupportedException();

            Assert.Equal("The clause is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void Message_Constructor()
        {
            ClauseNotSupportedException ex = new ClauseNotSupportedException("Custom message.");

            Assert.Equal("Custom message.", ex.Message);
        }

        [Fact]
        public void Message_And_Inner_Exception_Constructor()
        {
            Exception inner = new Exception("Inner exception");
            ClauseNotSupportedException ex = new ClauseNotSupportedException("Custom message.", inner);

            Assert.Equal("Custom message.", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }
}