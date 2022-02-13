using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class FunctionTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT")
                .Add(person["Name"])
                .Add(", ")
                .Add(person["SurName"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT").Add(person["Name"], ", ", person["SurName"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT").Add(new List<object> { person["Name"], ", ", person["SurName"] });

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IFunction func = sql.Function("CONCAT")
                .Add(() => person.Name)
                .Add(() => ", ")
                .Add(() => person.SurName);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IFunction func = sql.Function("CONCAT").Add(() => person.Name, () => ", ", () => person.SurName);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IFunction func = sql.Function("CONCAT").Add(new List<Expression<Func<object>>> { () => person.Name, () => ", ",
                () => person.SurName });

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT").Add(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CONCAT", person.Name, ", ", person.SurName));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Before()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("COUNT")
                .Before(sql.Raw("DISTINCT"))
                .Add(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(DISTINCT \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Only_Registered()
        {
            engine.Options.FunctionsOnlyRegistered = true;

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CONCAT", person.Name, ", ", person.SurName));

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => engine.Compile(func));
            Assert.Equal("Function \"CONCAT\" is not registered.", ex.Message);

            engine.AddFunction("CONCAT");
            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Translation()
        {
            engine.AddFunction("CUSTOM", "TRANSLATED");

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CUSTOM", person.Name, ", ", person.SurName));

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRANSLATED(\"person\".\"Name\", @p0, \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ", "
            }, result.Parameters);
        }

        [Fact]
        public void Delegate()
        {
            engine.AddFunction("CAST", (queryBuilder, engine, name, fn) =>
            {
                queryBuilder.Write(name + "(");
                queryBuilder.WriteValue(fn.Args[0]);
                queryBuilder.Write(" AS ");
                queryBuilder.WriteValue(fn.Args[1]);
                queryBuilder.Write(")");
            });

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CAST", person.Salary, sql.Type("VARCHAR")));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CAST(\"person\".\"Salary\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delegate_Translation()
        {
            engine.AddFunction("CAST", "CUSTOM", (queryBuilder, engine, name, fn) =>
            {
                queryBuilder.Write(name + "(");
                queryBuilder.WriteValue(fn.Args[0]);
                queryBuilder.Write(" AS ");
                queryBuilder.WriteValue(fn.Args[1]);
                queryBuilder.Write(")");
            });

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CAST", person.Salary, sql.Type("VARCHAR")));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CUSTOM(\"person\".\"Salary\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Delegate_Translation_Empty()
        {
            engine.AddFunction("CAST", null, (queryBuilder, engine, name, fn) =>
            {
                queryBuilder.Write(name + "(");
                queryBuilder.WriteValue(fn.Args[0]);
                queryBuilder.Write(" AS ");
                queryBuilder.WriteValue(fn.Args[1]);
                queryBuilder.Write(")");
            });

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CAST", person.Salary, sql.Type("VARCHAR")));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CAST(\"person\".\"Salary\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SubOperator()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT").Add(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Id\" > @p0, \"person\".\"Id\" < @p1)", result.Sql);
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
            IFunction func = sql.Function("CONCAT").Add(sql.Add.Add(person["Salary"], 1000m),
                sql.Multiply.Add(person["Salary"], 2m));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Salary\" + @p0, \"person\".\"Salary\" * @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m,
                ["@p1"] = 2m
            }, result.Parameters);
        }

        [Fact]
        public void SubQuery()
        {
            IFunction func = sql.Function("CONCAT").Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT((Subquery1), (Subquery2))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_List()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT");
            object[] values = new object[] { person["Name"], ", ", person["SurName"] };

            int i = 0;
            Assert.Equal(i, func.Count);

            foreach (object value in values)
            {
                func.Add(value);
                Assert.Equal(++i, func.Count);
            }
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT")
                .Add(person["Name"])
                .Add(", ")
                .Add(person["SurName"]);

            Assert.Equal("CONCAT(person.Name, \", \", person.SurName)", func.ToString());
        }

        [Fact]
        public void To_String_One_Value()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT").Add(person["Name"]);

            Assert.Equal("CONCAT(person.Name)", func.ToString());
        }

        [Fact]
        public void To_String_Before()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("COUNT")
                .Before(sql.Raw("DISTINCT"))
                .Add(person["Name"]);

            Assert.Equal("COUNT(DISTINCT person.Name)", func.ToString());
        }
    }
}