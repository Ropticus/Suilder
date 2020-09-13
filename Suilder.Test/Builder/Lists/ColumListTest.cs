using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Lists
{
    public class ColumnListTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IColList list = sql.ColList
                .Add(person["Id"])
                .Add(person["Active"])
                .Add(person["Name"]);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IColList list = sql.ColList.Add(person["Id"], person["Active"], person["Name"]);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IColList list = sql.ColList.Add(new List<IColumn> { person["Id"], person["Active"], person["Name"] });

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IColList list = sql.ColList
                .Add(() => person.Id)
                .Add(() => person.Active)
                .Add(() => person.Name);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IColList list = sql.ColList.Add(() => person.Id, () => person.Active, () => person.Name);

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IColList list = sql.ColList.Add(new List<Expression<Func<object>>> { () => person.Id, () => person.Active,
                () => person.Name});

            QueryResult result = engine.Compile(list);

            Assert.Equal("\"person\".\"Id\", \"person\".\"Active\", \"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            IColList list = sql.ColList;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(list));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IColList list = sql.ColList
                .Add(person["Id"])
                .Add(person["Active"])
                .Add(person["Name"]);

            Assert.Equal("person.Id, person.Active, person.Name", list.ToString());
        }
    }
}