using Suilder.Reflection;

namespace Suilder.Test.Reflection.TableNested.Tables
{
    [Nested]
    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }
    }
}