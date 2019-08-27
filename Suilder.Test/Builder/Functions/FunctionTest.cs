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
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT")
                .Add(person["Name"])
                .Add(", ")
                .Add(person["SurName"]);

            Assert.Equal("CONCAT(person.Name, \", \", person.SurName)", func.ToString());
        }
    }
}