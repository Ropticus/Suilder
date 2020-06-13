using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class GtTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt(person["Id"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Builder_Object_Enumerable<T>(T[] value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt(person["Image"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Column()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.Gt(person["DepartmentId"], dept["Id"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" > NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Left_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt((object)null, person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NULL > \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Expression(object value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Id, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Expression_ForeignKey(object value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Department.Id, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Expression_Nested(object value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Address.Street, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Expression_Nested_Deep(object value)
        {
            Person2 person = null;
            IOperator op = sql.Gt(() => person.Address.City.Country.Name, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Builder_Expression_Enumerable<T>(T[] value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Image, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Column()
        {
            Person person = null;
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.Gt(() => person.Department.Id, dept["Id"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Right_Null()
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" > NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Two_Expressions(object value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Id, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Two_Expressions_ForeignKey(object value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Department.Id, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Two_Expressions_Nested(object value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Address.Street, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Two_Expressions_Nested_Deep(object value)
        {
            Person2 person = null;
            IOperator op = sql.Gt(() => person.Address.City.Country.Name, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Builder_Two_Expressions_Enumerable<T>(T[] value)
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Image, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Gt(() => person.Department.Id, () => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Right_Value_Null()
        {
            Person person = null;
            IOperator op = sql.Gt(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" > NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].Gt(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Extension_Object_Enumerable<T>(T[] value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].Gt(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Gt(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" > NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Expression(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Id"].Gt(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Extension_Expression_Enumerable<T>(T[] value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].Gt(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Column()
        {
            IAlias person = sql.Alias("person");
            Department dept = null;
            IOperator op = person["DepartmentId"].Gt(() => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Gt(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" > NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression(int value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id > value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id > 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_ForeignKey(int value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Department.Id > value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Nested(int value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Address.Number > value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressNumber\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Nested_Deep(int value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Address.City.Country.Number > value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryNumber\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Op(() => person.Department.Id > dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method(object value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Id, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Id, 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method_ForeignKey(object value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Department.Id, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method_Nested(object value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Address.Street, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method_Nested_Deep(object value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Address.City.Country.Name, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Expression_Method_Enumerable<T>(T[] value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Image, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Enumerable_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Image, new byte[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Department.Id, dept.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" > \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Gt(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" > NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlExp.Gt(person.Id, 1));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Subquery()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt(person["Id"], sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" > (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt(person["Id"], 1);

            Assert.Equal("person.Id > 1", op.ToString());
        }

        [Fact]
        public void To_String_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt(person["Id"], new byte[] { 1, 2, 3 });

            Assert.Equal("person.Id > [1, 2, 3]", op.ToString());
        }
    }
}