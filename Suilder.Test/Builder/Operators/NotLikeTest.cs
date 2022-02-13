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
    public class NotLikeTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Name"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(person["Image"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(dept["Tags"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(person["DepartmentGuid"], dept["Guid"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Left_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike((object)null, person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("@p0 NOT LIKE \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Expression(object value)
        {
            Person person = null;
            IOperator op = sql.NotLike(() => person.Name, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Department.Guid, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Address.Street, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Address.City.Country.Name, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Image, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => dept.Tags, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Department.Guid, dept["Guid"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Right_Null()
        {
            Person person = null;
            IOperator op = sql.NotLike(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Two_Expressions(object value)
        {
            Person person = null;
            IOperator op = sql.NotLike(() => person.Name, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Department.Guid, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Address.Street, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Address.City.Country.Name, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Image, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => dept.Tags, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.NotLike(() => person.Department.Guid, () => dept.Guid);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Right_Value_Null()
        {
            Person person = null;
            IOperator op = sql.NotLike(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotLike(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
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
            IOperator op = person["Image"].NotLike(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT LIKE @p0", result.Sql);
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
            IOperator op = dept["Tags"].NotLike(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotLike(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Expression(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotLike(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
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
            IOperator op = person["Image"].NotLike(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT LIKE @p0", result.Sql);
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
            IOperator op = dept["Tags"].NotLike(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT LIKE @p0", result.Sql);
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
            IOperator op = person["DepartmentGuid"].NotLike(() => dept.Guid);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].NotLike(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression(string value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.NotLike(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.NotLike("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => person.Department.Guid.NotLike(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => person.Address.Street.NotLike(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => person.Address.City.Country.Name.NotLike(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => person.Department.Guid.NotLike(dept.Guid));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [InlineData(null)]
        public void Expression_Null(string value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.NotLike(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Function()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.NotLike(SqlExp.Trim(person.SurName)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE TRIM(\"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.NotLike(SqlExp.As<string>(person.Id)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE \"person\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Name.NotLike(SqlExp.As<string>(query)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Name.NotLike(SqlExp.Cast<string>(person.Id, sql.Type("VARCHAR"))));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE CAST(\"person\".\"Id\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Name.NotLike(SqlExp.Cast<string>(query, sql.Type("VARCHAR"))));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE CAST((Subquery) AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => person.Name.NotLike("%abcd%"));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method(object value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Name, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Name, "%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Department.Guid, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Address.Street, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Address.City.Country.Name, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Image, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Array_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Image, new byte[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.NotLike(dept.Tags, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_List_Inline_Value()
        {
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.NotLike(dept.Tags, new List<string> { "abcd", "efgh", "ijkl" }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" NOT LIKE @p0", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Department.Guid, dept.Guid));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentGuid\" NOT LIKE \"dept\".\"Guid\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => SqlExp.NotLike(person.Name, query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.NotLike(person.Name, "%abcd%"));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(DataString))]
        public void Expression_Val_Method(string value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Name.NotLike(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" > @p0) NOT LIKE (\"person\".\"Id\" < @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + @p0) NOT LIKE (\"person\".\"Salary\" * @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m,
                ["@p1"] = 2m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IAlias person = sql.Alias("person");
            IOperator op1 = sql.NotLike(person["Name"], sql.RawQuery("Subquery"));
            IOperator op2 = sql.NotLike(sql.RawQuery("Subquery"), person["Name"]);
            IOperator op3 = sql.NotLike(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result;

            result = engine.Compile(op1);

            Assert.Equal("\"person\".\"Name\" NOT LIKE (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(op2);

            Assert.Equal("(Subquery) NOT LIKE \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(op3);

            Assert.Equal("(Subquery1) NOT LIKE (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Name"], "%abcd%");

            Assert.Equal("person.Name NOT LIKE \"%abcd%\"", op.ToString());
        }

        [Fact]
        public void To_String_Array()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Image"], new byte[] { 1, 2, 3 });

            Assert.Equal("person.Image NOT LIKE [1, 2, 3]", op.ToString());
        }

        [Fact]
        public void To_String_List()
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.NotLike(dept["Tags"], new List<string> { "abcd", "efgh", "ijkl" });

            Assert.Equal("dept.Tags NOT LIKE [\"abcd\", \"efgh\", \"ijkl\"]", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            Assert.Equal("(person.Id > 10) NOT LIKE (person.Id < 20)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            Assert.Equal("(person.Salary + 1000) NOT LIKE (person.Salary * 2)", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IOperator op1 = sql.NotLike(person["Name"], sql.RawQuery("Subquery"));
            IOperator op2 = sql.NotLike(sql.RawQuery("Subquery"), person["Name"]);
            IOperator op3 = sql.NotLike(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            Assert.Equal("person.Name NOT LIKE (Subquery)", op1.ToString());
            Assert.Equal("(Subquery) NOT LIKE person.Name", op2.ToString());
            Assert.Equal("(Subquery1) NOT LIKE (Subquery2)", op3.ToString());
        }
    }
}