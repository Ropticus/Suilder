using Suilder.Reflection;

namespace Suilder.Performance.Tables
{
    [Nested]
    public class Address
    {
        public string Street { get; set; }

        public int Number { get; set; }

        public string City { get; set; }

        [Ignore]
        public string Ignore { get; set; }
    }
}