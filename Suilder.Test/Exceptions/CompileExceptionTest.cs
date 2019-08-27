using System;
using Suilder.Exceptions;
using Xunit;

namespace Suilder.Test.Exceptions
{
    public class CompileExceptionTest
    {
        [Fact]
        public void Default_Constructor()
        {
            CompileException ex = new CompileException();

            Assert.Equal("Cannot compile the query.", ex.Message);
        }

        [Fact]
        public void Message_Constructor()
        {
            CompileException ex = new CompileException("Custom message.");

            Assert.Equal("Custom message.", ex.Message);
        }

        [Fact]
        public void Message_And_Inner_Exception_Constructor()
        {
            Exception inner = new Exception("Inner exception");
            CompileException ex = new CompileException("Custom message.", inner);

            Assert.Equal("Custom message.", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }
}