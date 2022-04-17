using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Operators;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class InTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(person["Id"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Builder_Object_List<T>(List<T> value)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.In(dept["Name"], value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Column()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.In(person["DepartmentId"], dept["Id"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (\"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Right_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Name"], null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IN (@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Left_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In((object)null, person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("@p0 IN (\"person\".\"Name\")", result.Sql);
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
            IOperator op = sql.In(() => person.Id, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Department.Id, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Address.Street, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Address.City.Country.Name, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Id, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Builder_Expression_List<T>(List<T> value)
        {
            Department dept = null;
            IOperator op = sql.In(() => dept.Name, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Column()
        {
            Person person = null;
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.In(() => person.Department.Id, dept["Id"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (\"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Right_Null()
        {
            Person person = null;
            IOperator op = sql.In(() => person.Name, null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Id, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Department.Id, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Address.Street, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Address.City.Country.Name, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IN (@p0)", result.Sql);
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
            IOperator op = sql.In(() => person.Id, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Builder_Two_Expressions_List<T>(List<T> value)
        {
            Department dept = null;
            IOperator op = sql.In(() => dept.Name, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.In(() => person.Department.Id, () => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (\"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Two_Expressions_Right_Value_Null()
        {
            Person person = null;
            IOperator op = sql.In(() => person.Name, () => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IN (@p0)", result.Sql);
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
            IOperator op = person["Id"].In(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
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
            IOperator op = person["Id"].In(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Extension_Object_List<T>(List<T> value)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = dept["Name"].In(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].In(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IN (@p0)", result.Sql);
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
            IOperator op = person["Id"].In(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
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
            IOperator op = person["Id"].In(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Extension_Expression_List<T>(List<T> value)
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = dept["Name"].In(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Column()
        {
            IAlias person = sql.Alias("person");
            Department dept = null;
            IOperator op = person["DepartmentId"].In(() => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (\"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].In(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IN (@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataIntArray))]
        public void Expression_Array(int[] value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id.In(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Array_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id.In(new int[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataStringList))]
        public void Expression_List(List<string> value)
        {
            Department dept = null;
            IOperator op = sql.Op(() => dept.Name.In(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Expression_List_Inline_Value()
        {
            Department dept = null;
            IOperator op = sql.Op(() => dept.Name.In(new List<string> { "abcd", "efgh", "ijkl" }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh",
                ["@p2"] = "ijkl"
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataIntArray))]
        public void Expression_Array_ForeignKey(int[] value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Department.Id.In(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataStringArray))]
        public void Expression_Array_Nested(string[] value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Address.Street.In(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataStringArray))]
        public void Expression_Array_Nested_Deep(string[] value)
        {
            Person2 person = null;
            IOperator op = sql.Op(() => person.Address.City.Country.Name.In(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Theory]
        [InlineData(null)]
        public void Expression_Null(int[] value)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id.In(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Function_As()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id.In(SqlExp.As<int[]>(person.Name)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Id.In(SqlExp.As<int[]>(query)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id.In(SqlExp.Cast<int[]>(person.Name, sql.Type("INT"))));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (CAST(\"person\".\"Name\" AS INT))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Id.In(SqlExp.Cast<int[]>(query, sql.Type("INT"))));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (CAST((Subquery) AS INT))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => person.Id.In(new int[] { 1, 2, 3 }));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Expression_Method(object value)
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.In(person.Id, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.In(person.Id, 1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0)", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.In(person.Department.Id, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (@p0)", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.In(person.Address.Street, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressStreet\" IN (@p0)", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.In(person.Address.City.Country.Name, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryName\" IN (@p0)", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.In(person.Id, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Array_Inline_Value()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.In(person.Id, new int[] { 1, 2, 3 }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataList))]
        public void Expression_Method_List<T>(List<T> value)
        {
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.In(dept.Name, value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_List_Inline_Value()
        {
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.In(dept.Name, new List<string> { "abcd", "efgh", "ijkl" }));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Name\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh",
                ["@p2"] = "ijkl"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Method_Column()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Op(() => SqlExp.In(person.Department.Id, dept.Id));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" IN (\"dept\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Null()
        {
            Person person = null;
            IOperator op = sql.Op(() => SqlExp.In(person.Name, null));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" IN (@p0)", result.Sql);
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
            IOperator op = sql.Op(() => SqlExp.In(person.Id, query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Method_Invalid_Call()
        {
            Person person = new Person();

            Exception ex = Assert.Throws<NotSupportedException>(() => SqlExp.In(person.Id, new int[] { 1, 2, 3 }));
            Assert.Equal("Only for expressions.", ex.Message);
        }

        [Theory]
        [MemberData(nameof(DataIntArray))]
        public void Expression_Val_Method(int[] value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id.In(value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value[0],
                ["@p1"] = value[1],
                ["@p2"] = value[2]
            }, result.Parameters);
        }

        [Fact]
        public void Translation()
        {
            engine.AddOperator(OperatorName.In, "TRANSLATED");

            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" TRANSLATED (@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Array()
        {
            engine.AddOperator(OperatorName.In, "TRANSLATED");

            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], new int[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" TRANSLATED (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Function()
        {
            engine.AddOperator(OperatorName.In, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED(\"person\".\"Id\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Function_Array()
        {
            engine.AddOperator(OperatorName.In, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], new int[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED(\"person\".\"Id\", @p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" > @p0) IN (\"person\".\"Id\" < @p1)", result.Sql);
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
            IOperator op = sql.In(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + @p0) IN (\"person\".\"Salary\" * @p1)", result.Sql);
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
            IOperator op1 = sql.In(person["Id"], sql.RawQuery("Subquery"));
            IOperator op2 = sql.In(sql.RawQuery("Subquery"), person["Id"]);
            IOperator op3 = sql.In(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result;

            result = engine.Compile(op1);

            Assert.Equal("\"person\".\"Id\" IN (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(op2);

            Assert.Equal("(Subquery) IN (\"person\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = engine.Compile(op3);

            Assert.Equal("(Subquery1) IN (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], 1);

            Assert.Equal("person.Id IN (1)", op.ToString());
        }

        [Fact]
        public void To_String_Array()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], new int[] { 1, 2, 3 });

            Assert.Equal("person.Id IN (1, 2, 3)", op.ToString());
        }

        [Fact]
        public void To_String_List()
        {
            IAlias dept = sql.Alias("dept");
            IOperator op = sql.In(dept["Name"], new List<string> { "abcd", "efgh", "ijkl" });

            Assert.Equal("dept.Name IN (\"abcd\", \"efgh\", \"ijkl\")", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            Assert.Equal("(person.Id > 10) IN (person.Id < 20)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            Assert.Equal("(person.Salary + 1000) IN (person.Salary * 2)", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IOperator op1 = sql.In(person["Id"], sql.RawQuery("Subquery"));
            IOperator op2 = sql.In(sql.RawQuery("Subquery"), person["Id"]);
            IOperator op3 = sql.In(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            Assert.Equal("person.Id IN (Subquery)", op1.ToString());
            Assert.Equal("(Subquery) IN (person.Id)", op2.ToString());
            Assert.Equal("(Subquery1) IN (Subquery2)", op3.ToString());
        }
    }
}