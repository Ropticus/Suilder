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
    public class LikeTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Builder_Object_Array<T>(T[] value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Image"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Builder_Object_List<T>(List<T> value)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.Like(dept["Tags"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" LIKE @p0", result.Sql);
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
            IOperator op = sql.Like(person["DepartmentGuid"], dept["Guid"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Left_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like((object)null, person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("NULL LIKE \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Expression(object value)
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Name, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Expression_ForeignKey(object value)
        {
            Person2 person = null;
            IOperator op = sql.Like(() => person.Department.Guid, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE @p0", result.Sql);
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
            IOperator op = sql.Like(() => person.Address.Street, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" LIKE @p0", result.Sql);
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
            IOperator op = sql.Like(() => person.Address.City.Country.Name, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Builder_Expression_Array<T>(T[] value)
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Image, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Builder_Expression_List<T>(List<T> value)
        {
            Department dept = null;
            IOperator op = sql.Like(() => dept.Tags, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Column()
        {
            Person2 person = null;
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.Like(() => person.Department.Guid, dept["Guid"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Right_Null()
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Two_Expressions(object value)
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Name, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Two_Expressions_ForeignKey(object value)
        {
            Person2 person = null;
            IOperator op = sql.Like(() => person.Department.Guid, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE @p0", result.Sql);
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
            IOperator op = sql.Like(() => person.Address.Street, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" LIKE @p0", result.Sql);
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
            IOperator op = sql.Like(() => person.Address.City.Country.Name, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Builder_Two_Expressions_Array<T>(T[] value)
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Image, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Builder_Two_Expressions_List<T>(List<T> value)
        {
            Department dept = null;
            IOperator op = sql.Like(() => dept.Tags, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Column()
        {
            Person2 person = null;
            Department2 dept = null;
            IOperator op = sql.Like(() => person.Department.Guid, () => dept.Guid);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Right_Value_Null()
        {
            Person person = null;
            IOperator op = sql.Like(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Like(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Extension_Object_Array<T>(T[] value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].Like(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Extension_Object_List<T>(List<T> value)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = dept["Tags"].Like(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Like(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Expression(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Like(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Extension_Expression_Array<T>(T[] value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Image"].Like(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Extension_Expression_List<T>(List<T> value)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = dept["Tags"].Like(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Column()
        {
            IAlias person = sql.Alias("person");
            Department2 dept = null;
            IOperator op = person["DepartmentGuid"].Like(() => dept.Guid);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Like(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression(string value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.Like(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "%abcd%"
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_ForeignKey(string value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Department.Guid.Like(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Nested(string value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Address.Street.Like(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Nested_Deep(string value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Address.City.Country.Name.Like(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Column()
        {
            Person2 person = null;
            Department2 dept = null;
            IOperator op = sql.Op(() => person.Department.Guid.Like(dept.Guid));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method(object value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Name, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Name, "%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "%abcd%"
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method_ForeignKey(object value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Department.Guid, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.Like(person.Address.Street, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.Like(person.Address.City.Country.Name, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataArray))]
        public void Expression_Method_Array<T>(T[] value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Image, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Array_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Image, new byte[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new byte[] { 1, 2, 3 }
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Expression_Method_List<T>(List<T> value)
        {
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.Like(dept.Tags, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_List_Inline_Value()
        {
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.Like(dept.Tags, new List<string> { "abcd", "efgh", "ijkl" }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new List<string> { "abcd", "efgh", "ijkl" }
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Column()
        {
            Person2 person = null;
            Department2 dept = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Department.Guid, dept.Guid));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.Like(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<InvalidOperationException>(() => person.Name.Like("%abcd%"));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<InvalidOperationException>(() => SqlExp.Like(person.Name, "%abcd%"));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Fact]
        public void Subquery()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" LIKE (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], "%abcd%");

            Assert.Equal("person.Name LIKE \"%abcd%\"", op.ToString());
        }

        [Fact]
        public void To_String_Array()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Image"], new byte[] { 1, 2, 3 });

            Assert.Equal("person.Image LIKE [1, 2, 3]", op.ToString());
        }

        [Fact]
        public void To_String_List()
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.Like(dept["Tags"], new List<string> { "abcd", "efgh", "ijkl" });

            Assert.Equal("dept.Tags LIKE [\"abcd\", \"efgh\", \"ijkl\"]", op.ToString());
        }
    }
}