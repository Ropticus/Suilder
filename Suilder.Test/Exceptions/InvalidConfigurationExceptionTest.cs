using System;
using Suilder.Exceptions;
using Xunit;

namespace Suilder.Test.Exceptions
{
    public class InvalidConfigurationExceptionTest
    {
        [Fact]
        public void Default_Constructor()
        {
            InvalidConfigurationException ex = new InvalidConfigurationException();

            Assert.Equal("Invalid configuration.", ex.Message);
        }

        [Fact]
        public void Message_Constructor()
        {
            InvalidConfigurationException ex = new InvalidConfigurationException("Custom message.");

            Assert.Equal("Custom message.", ex.Message);
        }

        [Fact]
        public void Message_And_Inner_Exception_Constructor()
        {
            Exception inner = new Exception("Inner exception");
            InvalidConfigurationException ex = new InvalidConfigurationException("Custom message.", inner);

            Assert.Equal("Custom message.", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }
}