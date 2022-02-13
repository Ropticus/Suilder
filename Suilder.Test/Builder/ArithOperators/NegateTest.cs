using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.ArithOperators
{
    public class NegateTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(person["Salary"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Builder_Object_Value(object value)
        {
            IOperator op = sql.Negate(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Object_Function()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(SqlFn.Max(person["Salary"]));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- MAX(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Object_Null()
        {
            IOperator op = sql.Negate(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression()
        {
            Person person = null;
            IOperator op = sql.Negate(() => person.Salary);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Builder_Expression_Value(int value)
        {
            IOperator op = sql.Negate(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Inline_Value()
        {
            IOperator op = sql.Negate(() => 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Builder_Expression_ForeignKey()
        {
            Person person = null;
            IOperator op = sql.Negate(() => person.Department.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Nested()
        {
            Person person = null;
            IOperator op = sql.Negate(() => person.Address.Number);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"AddressNumber\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = sql.Negate(() => person.Address.City.Country.Number);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"AddressCityCountryNumber\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Builder_Expression_Function()
        {
            Person person = null;
            IOperator op = sql.Negate(() => SqlExp.Max(person.Salary));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- MAX(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Object()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Salary"].Negate();

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -person.Salary);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Value(int value)
        {
            IOperator op = (IOperator)sql.Val(() => -value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            int value = (int)sql.Val(() => -1);
            Assert.Equal(-1, value);
        }

        [Fact]
        public void Expression_ForeignKey()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -person.Department.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -person.Address.Number);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"AddressNumber\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Deep()
        {
            Person2 person = null;
            IOperator op = (IOperator)sql.Val(() => -person.Address.City.Country.Number);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"AddressCityCountryNumber\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -SqlExp.Max(person.Salary));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- MAX(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -SqlExp.As<decimal>(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_SubQuery()
        {
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => -SqlExp.As<decimal>(query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => -SqlExp.Cast<decimal>(person.Name, sql.Type("DECIMAL", 10, 2)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- CAST(\"person\".\"Name\" AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_SubQuery()
        {
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => -SqlExp.Cast<decimal>(query, sql.Type("DECIMAL", 10, 2)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- CAST((Subquery) AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(sql.Gt(person["Id"], 10));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- (\"person\".\"Id\" > @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(sql.Add.Add(person["Salary"], 1000m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- (\"person\".\"Salary\" + @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IOperator op = sql.Negate(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("- (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(person["Salary"]);

            Assert.Equal("- person.Salary", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(sql.Gt(person["Id"], 10));

            Assert.Equal("- (person.Id > 10)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(sql.Add.Add(person["Salary"], 1000m));

            Assert.Equal("- (person.Salary + 1000)", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IOperator op = sql.Negate(sql.RawQuery("Subquery"));

            Assert.Equal("- (Subquery)", op.ToString());
        }
    }
}