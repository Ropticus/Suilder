using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.LogicalOperators
{
    public class OrTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(person["Id"].Eq(1), person["Active"].Eq(true), person["Name"].Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(new List<IQueryFragment> { person["Id"].Eq(1), person["Active"].Eq(true),
                person["Name"].Like("%abcd%") });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IOperator op = sql.Or
                .Add(() => person.Id == 1)
                .Add(() => person.Active)
                .Add(() => person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IOperator op = sql.Or.Add(() => person.Id == 1, () => person.Active, () => person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IOperator op = sql.Or.Add(new List<Expression<Func<bool>>> { () => person.Id == 1, () => person.Active,
                () => person.Name.Like("%abcd%")});

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(person["Id"].Eq(1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 | person.Active);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 || person.Active);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_Values()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 | person.Active | person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Values()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 || person.Active || person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Theory]
        [InlineData(1000, 10, "efgh")]
        public void Expression_Or_Large(decimal value1, int value2, string value3)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 | person.Active | person.Name.Like("%abcd%")
                | person.Salary > value1 | person.Address.Number == value2 | person.Address.City == value3);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2 "
                + "OR \"person\".\"Salary\" > @p3 OR \"person\".\"AddressNumber\" = @p4 "
                + "OR \"person\".\"AddressCity\" = @p5", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%",
                ["@p3"] = value1,
                ["@p4"] = value2,
                ["@p5"] = value3
            }, result.Parameters);
        }

        [Theory]
        [InlineData(1000, 10, "efgh")]
        public void Expression_OrElse_Large(decimal value1, int value2, string value3)
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 || person.Active || person.Name.Like("%abcd%")
                || person.Salary > value1 || person.Address.Number == value2 || person.Address.City == value3);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2 "
                + "OR \"person\".\"Salary\" > @p3 OR \"person\".\"AddressNumber\" = @p4 "
                + "OR \"person\".\"AddressCity\" = @p5", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%",
                ["@p3"] = value1,
                ["@p4"] = value2,
                ["@p5"] = value3
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_OrElse()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 | person.Active || person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Or()
        {
            Person person = null;
            IOperator op = sql.Op(() => (person.Id == 1 || person.Active) | person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_Function()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active | SqlExp.Min(person.Active));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR MIN(\"person\".\"Active\") = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Function()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active || SqlExp.Min(person.Active));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR MIN(\"person\".\"Active\") = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_Function_As()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active | SqlExp.As<bool>(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR \"person\".\"Name\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Function_As()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active || SqlExp.As<bool>(person.Name));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR \"person\".\"Name\" = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_Function_As_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Active | SqlExp.As<bool>(query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR (Subquery) = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Function_As_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Active || SqlExp.As<bool>(query));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR (Subquery) = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_Function_Cast()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active | SqlExp.Cast<bool>(person.Name, sql.Type("BIT")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR CAST(\"person\".\"Name\" AS BIT) = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Function_Cast()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Active || SqlExp.Cast<bool>(person.Name, sql.Type("BIT")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR CAST(\"person\".\"Name\" AS BIT) = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_Function_Cast_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Active | SqlExp.Cast<bool>(query, sql.Type("BIT")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR CAST((Subquery) AS BIT) = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Function_Cast_SubQuery()
        {
            Person person = null;
            IRawQuery query = sql.RawQuery("Subquery");
            IOperator op = sql.Op(() => person.Active || SqlExp.Cast<bool>(query, sql.Type("BIT")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Active\" = @p0 OR CAST((Subquery) AS BIT) = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true,
                ["@p1"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Expression_Or_Val_Method()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id == 1 | person.Active | person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_OrElse_Val_Method()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Id == 1 || person.Active || person.Name.Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 OR \"person\".\"Active\" = @p1 OR \"person\".\"Name\" LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(sql.And.Add(person["Id"].Gt(10), person["Id"].Lt(20)),
                sql.Or.Add(person["Active"].Eq(true), person["Name"].Like("%abcd%")));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(\"person\".\"Id\" > @p0 AND \"person\".\"Id\" < @p1) OR "
                + "(\"person\".\"Active\" = @p2 OR \"person\".\"Name\" LIKE @p3)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20,
                ["@p2"] = true,
                ["@p3"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IOperator op = sql.Or.Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(Subquery1) OR (Subquery2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_List()
        {
            IAlias person = sql.Alias("person");
            ILogicalOperator op = sql.Or;
            IOperator[] values = new IOperator[] { person["Id"].Eq(1), person["Active"].Eq(true),
                person["Name"].Like("%abcd%") };

            int i = 0;
            Assert.Equal(i, op.Count);

            foreach (IOperator value in values)
            {
                op.Add(value);
                Assert.Equal(++i, op.Count);
            }
        }

        [Fact]
        public void Empty_List()
        {
            IOperator op = sql.Or;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%abcd%"));

            Assert.Equal("person.Id = 1 OR person.Active = true OR person.Name LIKE \"%abcd%\"", op.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(person["Id"].Eq(1));

            Assert.Equal("person.Id = 1", op.ToString());
        }

        [Fact]
        public void To_String_SubOperator_List()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or.Add(sql.And.Add(person["Id"].Gt(10), person["Id"].Lt(20)),
                sql.Or.Add(person["Active"].Eq(true), person["Name"].Like("%abcd%")));

            Assert.Equal("(person.Id > 10 AND person.Id < 20) OR "
                + "(person.Active = true OR person.Name LIKE \"%abcd%\")", op.ToString());
        }

        [Fact]
        public void To_String_SubQuery()
        {
            IOperator op = sql.Or.Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            Assert.Equal("(Subquery1) OR (Subquery2)", op.ToString());
        }
    }
}