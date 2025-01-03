using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Operators;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.ArithOperators
{
    public class MultiplyTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add(decimal value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m)
                .Add(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Params(decimal value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(person["Salary"], 100m, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Enumerable(decimal value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(new List<object> { person["Salary"], 100m, value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Expression(decimal value)
        {
            Person person = null;
            IOperator op = sql.Multiply
                .Add(() => person.Salary)
                .Add(() => 100m)
                .Add(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Expression_Params(decimal value)
        {
            Person person = null;
            IOperator op = sql.Multiply.Add(() => person.Salary, () => 100m, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Add_Expression_Enumerable(decimal value)
        {
            Person person = null;
            IOperator op = sql.Multiply.Add(new List<Expression<Func<object>>> { () => person.Salary, () => 100m,
                () => value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(person["Salary"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Salary"].Multiply(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0", result.Sql);
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
            IOperator op = person["Image"].Multiply(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" * @p0", result.Sql);
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
            IOperator op = dept["Tags"].Multiply(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Multiply(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" * @p0", result.Sql);
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
            IOperator op = person["Salary"].Multiply(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0", result.Sql);
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
            IOperator op = person["Image"].Multiply(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" * @p0", result.Sql);
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
            IOperator op = dept["Tags"].Multiply(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" * @p0", result.Sql);
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
            IOperator op = person["DepartmentId"].Multiply(() => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" * \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].Multiply(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Expression(decimal value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataLong))]
        public void Expression_Checked(long value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 100m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value_Checked()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * 100L));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataDecimal))]
        public void Expression_Values(decimal value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 100m * value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataLong))]
        public void Expression_Values_Checked(long value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * 100L * value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Large(int value1, long value2, decimal value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 100m * value1 * value2 * value3 * 500);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1 * @p2 * @p3 * @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500m
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Large_Checked(int value1, long value2, long value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * 100L * value1 * value2 * value3 * 500));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0 * @p1 * @p2 * @p3 * @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500L
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Parentheses_Center(int value1, long value2, decimal value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 100m * (value1 * value2) * value3 * 500);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * (@p1 * @p2) * @p3 * @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500m
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Parentheses_Center_Checked(int value1, long value2, long value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * 100L * (value1 * value2) * value3 * 500));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0 * (@p1 * @p2) * @p3 * @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500L
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Parentheses_Right(decimal value1, long value2, int value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 100m * value1 * value2 * (value3 * 500));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1 * @p2 * (@p3 * @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Parentheses_Right_Checked(long value1, long value2, int value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * 100L * value1 * value2 * (value3 * 500)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0 * @p1 * @p2 * (@p3 * @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Parentheses_Both(int value1, long value2, short value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 100m * (value1 * value2) * (value3 * 500));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * (@p1 * @p2) * (@p3 * @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [InlineData(200, 300, 400)]
        public void Expression_Parentheses_Both_Checked(int value1, long value2, short value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * 100L * (value1 * value2) * (value3 * 500)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0 * (@p1 * @p2) * (@p3 * @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 500
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataLong))]
        public void Expression_Unchecked_Checked(long value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(unchecked(person.Id * 100L) * value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataLong))]
        public void Expression_Checked_Unchecked(long value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * 100L) * value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100L,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Function()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * SqlExp.Min(person.Salary));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * MIN(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Checked()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * SqlExp.Min(person.Id)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * MIN(\"person\".\"Id\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * SqlExp.As<decimal>(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_Checked()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * SqlExp.As<long>(person.Name)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => person.Salary * SqlExp.As<decimal>(query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_SubQuery_Checked()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * SqlExp.As<long>(query)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * SqlExp.Cast<decimal>(person.Name,
                sql.Type("DECIMAL", 10, 2)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * CAST(\"person\".\"Name\" AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_Checked()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * SqlExp.Cast<long>(person.Name,
                sql.Type("DECIMAL", 10, 2))));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * CAST(\"person\".\"Name\" AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => person.Salary * SqlExp.Cast<decimal>(query, sql.Type("DECIMAL", 10, 2)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * CAST((Subquery) AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_SubQuery_Checked()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => checked(person.Id * SqlExp.Cast<long>(query,
                sql.Type("DECIMAL", 10, 2))));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" * CAST((Subquery) AS DECIMAL(10, 2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation()
        {
            engine.AddOperator(OperatorName.Multiply, "TRANSLATED");

            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m)
                .Add(200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" TRANSLATED @p0 TRANSLATED @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200
            }, result.Parameters);
        }

        [Fact]
        public void Translation_One_Value()
        {
            engine.AddOperator(OperatorName.Multiply, "TRANSLATED");

            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(person["Salary"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation_Function()
        {
            engine.AddOperator(OperatorName.Multiply, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m)
                .Add(200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED(TRANSLATED(\"person\".\"Salary\", @p0), @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Function_Two_Values()
        {
            engine.AddOperator(OperatorName.Multiply, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED(\"person\".\"Salary\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Function_One_Value()
        {
            engine.AddOperator(OperatorName.Multiply, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(person["Salary"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" > @p0) * (\"person\".\"Id\" < @p1)", result.Sql);
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
            IOperator op = sql.Multiply.Add(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + @p0) * (\"person\".\"Salary\" * @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m,
                ["@p1"] = 2m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IOperator op = sql.Multiply.Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(Subquery1) * (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_List()
        {
            IAlias person = sql.Alias("person");
            IArithOperator op = sql.Multiply;
            object[] values = new object[] { person["Salary"], 100m, 200m };

            int i = 0;
            Assert.Equal(i, op.Count);

            foreach (object value in values)
            {
                op.Add(value);
                Assert.Equal(++i, op.Count);
            }
        }

        [Fact]
        public void Empty_List()
        {
            IOperator op = sql.Multiply;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m)
                .Add(200m);

            Assert.Equal("person.Salary * 100 * 200", op.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(person["Salary"]);

            Assert.Equal("person.Salary", op.ToString());
        }

        [Fact]
        public void To_String_Empty()
        {
            IOperator op = sql.Multiply;

            Assert.Equal("", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            Assert.Equal("(person.Id > 10) * (person.Id < 20)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            Assert.Equal("(person.Salary + 1000) * (person.Salary * 2)", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IOperator op = sql.Multiply.Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            Assert.Equal("(Subquery1) * (Subquery2)", op.ToString());
        }
    }
}