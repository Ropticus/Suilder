using Suilder.Reflection;

namespace Suilder.Test.Engines.Tables
{
    [Nested]
    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        [Ignore]
        public string Ignore { get; set; }
    }
}