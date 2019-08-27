using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ValListTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IValList list = sql.ValList
                .Add(person["Id"])
                .Add(1)
                .Add("text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", engine.Compile(list).Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IValList list = sql.ValList.Add(person["Id"], 1, "text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", engine.Compile(list).Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IValList list = sql.ValList.Add(new List<object> { person["Id"], 1, "text" });

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", engine.Compile(list).Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IValList list = sql.ValList
                .Add(() => person.Id)
                .Add(() => 1)
                .Add(() => "text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", engine.Compile(list).Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IValList list = sql.ValList.Add(() => person.Id, () => 1, () => "text");

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", engine.Compile(list).Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IValList list = sql.ValList.Add(new List<Expression<Func<object>>> { () => person.Id, () => 1, () => "text" });

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", @p0, @p1", engine.Compile(list).Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = "text"
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            IValList list = sql.ValList;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(list));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IValList list = sql.ValList
                .Add(person["Id"])
                .Add(1)
                .Add("text");

            Assert.Equal("person.Id, 1, \"text\"", list.ToString());
        }
    }
}