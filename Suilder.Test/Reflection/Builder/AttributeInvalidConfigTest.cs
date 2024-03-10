using System;
using Suilder.Exceptions;
using Suilder.Reflection;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class AttributeInvalidConfigTest : BaseTest
    {
        [Fact]
        public void Foreign_Key_Composite_Primitive()
        {
            tableBuilder.Add<ForeignKeyCompositePrimitive.Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Invalid multiple \"{typeof(ForeignKeyAttribute)}\" for property \"DepartmentId\" of the type "
                + $"\"{typeof(ForeignKeyCompositePrimitive.Person)}\".", ex.Message);
        }

        [Fact]
        public void Foreign_Key_Composite_Empty_Name()
        {
            tableBuilder.Add<ForeignKeyCompositeEmptyName.Person>();

            tableBuilder.Add<ForeignKeyCompositeEmptyName.Department>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"Empty property name in multiple \"{typeof(ForeignKeyAttribute)}\" for property \"Department\" "
                + $"of the type \"{typeof(ForeignKeyCompositeEmptyName.Person)}\".", ex.Message);
        }

        private class ForeignKeyCompositePrimitive
        {
            public class Person
            {
                public int Id { get; set; }

                public string Name { get; set; }

                [ForeignKey("Guid2")]
                [ForeignKey("Id2")]
                public int DepartmentId { get; set; }
            }
        }

        private class ForeignKeyCompositeEmptyName
        {
            public class Department
            {
                public int Id { get; set; }

                public string Name { get; set; }
            }

            public class Person
            {
                public int Id { get; set; }

                public string Name { get; set; }

                [ForeignKey("Guid2")]
                [ForeignKey("Id2")]
                public Department Department { get; set; }
            }
        }
    }
}