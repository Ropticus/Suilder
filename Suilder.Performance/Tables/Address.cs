using Suilder.Reflection;

namespace Suilder.Performance.Tables
{
    [Nested]
    public class Address
    {
        public string Street { get; set; }

        public int Number { get; set; }

        public City City { get; set; }

        [Ignore]
        public string Ignore { get; set; }
    }

    [Nested]
    public class City
    {
        public string Name { get; set; }

        public Country Country { get; set; }
    }

    [Nested]
    public class Country
    {
        public string Name { get; set; }

        public int Number { get; set; }
    }
}