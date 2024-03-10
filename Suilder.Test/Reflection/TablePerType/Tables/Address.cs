using Suilder.Reflection;

namespace Suilder.Test.Reflection.TablePerType.Tables
{
    [Nested]
    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }
    }
}