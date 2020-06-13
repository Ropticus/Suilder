using Suilder.Reflection;

namespace Suilder.Test.Builder.Tables
{
    [Nested]
    public class Address2
    {
        public string Street { get; set; }

        public int Number { get; set; }

        public City2 City { get; set; }
    }

    [Nested]
    public class City2
    {
        public string Name { get; set; }

        public Country2 Country { get; set; }
    }

    [Nested]
    public class Country2
    {
        public string Name { get; set; }

        public int Number { get; set; }
    }
}