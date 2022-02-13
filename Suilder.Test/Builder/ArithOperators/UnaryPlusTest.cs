using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.ArithOperators
{
    public class UnaryPlusTest : BuilderBaseTest
    {
        [Fact]
        public void Expression()
        {
            Person person = null;
            IColumn op = (IColumn)sql.Val(() => +person.Salary);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Value(int value)
        {
            int value2 = (int)sql.Val(() => +value);
            Assert.Equal(value, value2);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            int value = (int)sql.Val(() => +1);
            Assert.Equal(1, value);
        }

        [Fact]
        public void Expression_ForeignKey()
        {
            Person person = null;
            IColumn op = (IColumn)sql.Val(() => +person.Department.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested()
        {
            Person person = null;
            IColumn op = (IColumn)sql.Val(() => +person.Address.Number);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressNumber\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Nested_Deep()
        {
            Person2 person = null;
            IColumn op = (IColumn)sql.Val(() => +person.Address.City.Country.Number);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"AddressCityCountryNumber\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function()
        {
            Person person = null;
            IFunction op = (IFunction)sql.Val(() => +SqlExp.Max(person.Salary));

            QueryResult result = engine.Compile(op);

            Assert.Equal("MAX(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As()
        {
            Person person = null;
            IColumn op = (IColumn)sql.Val(() => +SqlExp.As<decimal>(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_SubQuery()
        {
            IRawQuery query = sql.RawQuery("Subquery");
            ISubQuery op = (ISubQuery)sql.Val(() => +SqlExp.As<decimal>(query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("Subquery", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast()
        {
            Person person = null;
            IFunction op = (IFunction)sql.Val(() => +SqlExp.Cast<decimal>(person.Name, sql.Type("DECIMAL", 10, 2)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("CAST(\"person\".\"Name\" AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_SubQuery()
        {
            IRawQuery query = sql.RawQuery("Subquery");
            IFunction op = (IFunction)sql.Val(() => +SqlExp.Cast<decimal>(query, sql.Type("DECIMAL", 10, 2)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("CAST((Subquery) AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Value_CustomType()
        {
            CustomType value = new CustomType(1);

            CustomType value2 = (CustomType)sql.Val(() => +value);
            Assert.Equal(value, value2);
        }

        private struct CustomType
        {
            public CustomType(int value) => Value = value;

            public int Value { get; set; }

            public static CustomType operator +(CustomType value) => value;
        }
    }
}