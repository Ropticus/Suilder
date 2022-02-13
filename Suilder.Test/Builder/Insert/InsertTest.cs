using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Insert
{
    public class InsertTest : BuilderBaseTest
    {
        [Fact]
        public void Insert_String()
        {
            IInsert insert = sql.Insert().Into("person");

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Alias()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person);

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Insert_Expression()
        {
            Person person = null;
            IInsert insert = sql.Insert().Into(() => person);

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"Person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person)
                .Add(person["Name"])
                .Add(person["SurName"]);

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"person\" (\"Name\", \"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person).Add(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"person\" (\"Name\", \"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person).Add(new List<IColumn> { person["Name"], person["SurName"] });

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"person\" (\"Name\", \"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IInsert insert = sql.Insert().Into(() => person)
                .Add(() => person.Name)
                .Add(() => person.SurName);

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IInsert insert = sql.Insert().Into(() => person).Add(() => person.Name, () => person.SurName);

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IInsert insert = sql.Insert().Into(() => person).Add(new List<Expression<Func<object>>> { () => person.Name,
                () => person.SurName });

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"Person\" (\"Name\", \"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_One_Value()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person).Add(person["Name"]);

            QueryResult result = engine.Compile(insert);

            Assert.Equal("INSERT INTO \"person\" (\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Into_Null()
        {
            IInsert insert = sql.Insert();

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(insert));
            Assert.Equal("Into value is null.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person);

            Assert.Equal("INSERT INTO person", insert.ToString());
        }

        [Fact]
        public void To_String_With_Columns()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person)
                .Add(person["Name"])
                .Add(person["SurName"]);

            Assert.Equal("INSERT INTO person (person.Name, person.SurName)", insert.ToString());
        }

        [Fact]
        public void To_String_With_Columns_One_Value()
        {
            IAlias person = sql.Alias("person");
            IInsert insert = sql.Insert().Into(person).Add(person["Name"]);

            Assert.Equal("INSERT INTO person (person.Name)", insert.ToString());
        }
    }
}