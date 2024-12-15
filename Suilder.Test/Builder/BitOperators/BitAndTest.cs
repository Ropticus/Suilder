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

namespace Suilder.Test.Builder.BitOperators
{
    public class BitAndTest : BuilderBaseTest
    {
        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add(int value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Flags"])
                .Add(1)
                .Add(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Params(int value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(person["Flags"], 1, value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Enumerable(int value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(new List<object> { person["Flags"], 1, value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Expression(int value)
        {
            Person person = null;
            IOperator op = sql.BitAnd
                .Add(() => person.Flags)
                .Add(() => 1)
                .Add(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Expression_Params(int value)
        {
            Person person = null;
            IOperator op = sql.BitAnd.Add(() => person.Flags, () => 1, () => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Add_Expression_Enumerable(int value)
        {
            Person person = null;
            IOperator op = sql.BitAnd.Add(new List<Expression<Func<object>>> { () => person.Flags, () => 1, () => value });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(person["Flags"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataObject))]
        public void Extension_Object(object value)
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Flags"].BitAnd(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0", result.Sql);
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
            IOperator op = person["Image"].BitAnd(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" & @p0", result.Sql);
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
            IOperator op = dept["Tags"].BitAnd(value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" & @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Extension_Object_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].BitAnd(null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" & @p0", result.Sql);
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
            IOperator op = person["Flags"].BitAnd(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0", result.Sql);
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
            IOperator op = person["Image"].BitAnd(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Image\" & @p0", result.Sql);
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
            IOperator op = dept["Tags"].BitAnd(() => value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"dept\".\"Tags\" & @p0", result.Sql);
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
            IOperator op = person["DepartmentId"].BitAnd(() => dept.Id);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"DepartmentId\" & \"dept\".\"Id\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Extension_Expression_Null()
        {
            IAlias person = sql.Alias("person");
            IOperator op = person["Name"].BitAnd(() => null);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Name\" & @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = null
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression(uint value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataEnum))]
        public void Expression_Enum(PersonFlags value)
        {
            Person2 person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Checked(uint value)
        {
            Person2 person = null;
            IOperator op = (IOperator)sql.Val(() => checked((ulong)person.Flags & value));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Inline_Value_Enum()
        {
            Person2 person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & PersonFlags.ValueA);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (int)PersonFlags.ValueA
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataInt))]
        public void Expression_Values(uint value)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul & value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [MemberData(nameof(DataEnum))]
        public void Expression_Values_Enum(PersonFlags value)
        {
            Person2 person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & PersonFlags.ValueA & value);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = (int)PersonFlags.ValueA,
                ["@p1"] = value
            }, result.Parameters);
        }

        [Theory]
        [InlineData(2, 3, 4)]
        public void Expression_Large(ushort value1, uint value2, ulong value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul & value1 & value2 & value3 & 5);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1 & @p2 & @p3 & @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 5ul
            }, result.Parameters);
        }

        [Theory]
        [InlineData(2, 3, 4)]
        public void Expression_Parentheses_Center(ushort value1, uint value2, ulong value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul & (value1 & value2) & value3 & 5);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & (@p1 & @p2) & @p3 & @p4", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 5ul
            }, result.Parameters);
        }

        [Theory]
        [InlineData(2, 3, 4)]
        public void Expression_Parentheses_Right(ulong value1, uint value2, ushort value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul & value1 & value2 & (value3 & 5u));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & @p1 & @p2 & (@p3 & @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 5u
            }, result.Parameters);
        }

        [Theory]
        [InlineData(2, 3, 4)]
        public void Expression_Parentheses_Both(uint value1, uint value2, ushort value3)
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & 1ul & (value1 & value2) & (value3 & 5u));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & @p0 & (@p1 & @p2) & (@p3 & @p4)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1ul,
                ["@p1"] = value1,
                ["@p2"] = value2,
                ["@p3"] = value3,
                ["@p4"] = 5u
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Function()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & SqlExp.Min(person.Flags));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & MIN(\"person\".\"Flags\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & SqlExp.As<ulong>(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_As_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => person.Flags & SqlExp.As<ulong>(query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Flags & SqlExp.Cast<ulong>(person.Name, sql.Type("BIGINT")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & CAST(\"person\".\"Name\" AS BIGINT)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression_Function_Cast_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = (IOperator)sql.Val(() => person.Flags & SqlExp.Cast<ulong>(query, sql.Type("BIGINT")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" & CAST((Subquery) AS BIGINT)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation()
        {
            engine.AddOperator(OperatorName.BitAnd, "TRANSLATED");

            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\" TRANSLATED @p0 TRANSLATED @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Translation_One_Value()
        {
            engine.AddOperator(OperatorName.BitAnd, "TRANSLATED");

            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(person["Flags"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Translation_Function()
        {
            engine.AddOperator(OperatorName.BitAnd, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED(TRANSLATED(\"person\".\"Flags\", @p0), @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Function_Two_Values()
        {
            engine.AddOperator(OperatorName.BitAnd, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Flags"])
                .Add(1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("TRANSLATED(\"person\".\"Flags\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Translation_Function_One_Value()
        {
            engine.AddOperator(OperatorName.BitAnd, "TRANSLATED", true);

            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(person["Flags"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Flags\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" > @p0) & (\"person\".\"Id\" < @p1)", result.Sql);
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
            IOperator op = sql.BitAnd.Add(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Salary\" + @p0) & (\"person\".\"Salary\" * @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m,
                ["@p1"] = 2m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IOperator op = sql.BitAnd.Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(Subquery1) & (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_List()
        {
            IAlias person = sql.Alias("person");
            IBitOperator op = sql.BitAnd;
            object[] values = new object[] { person["Flags"], 1, 2 };

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
            IOperator op = sql.BitAnd;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            Assert.Equal("person.Flags & 1 & 2", op.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(person["Flags"]);

            Assert.Equal("person.Flags", op.ToString());
        }

        [Fact]
        public void To_String_Empty()
        {
            IOperator op = sql.BitAnd;

            Assert.Equal("", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            Assert.Equal("(person.Id > 10) & (person.Id < 20)", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd.Add(sql.Add.Add(person["Salary"], 1000m), sql.Multiply.Add(person["Salary"], 2m));

            Assert.Equal("(person.Salary + 1000) & (person.Salary * 2)", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IOperator op = sql.BitAnd.Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            Assert.Equal("(Subquery1) & (Subquery2)", op.ToString());
        }
    }
}