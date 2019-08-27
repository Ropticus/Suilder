using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class DeleteTest : BuilderBaseTest
    {
        [Fact]
        public void Delete()
        {
            IDelete delete = sql.Delete();

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Top()
        {
            IDelete delete = sql.Delete().Top(10);

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Percent()
        {
            IDelete delete = sql.Delete().Top(10).Percent();

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE TOP(@p0) PERCENT", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_WithTies()
        {
            IDelete delete = sql.Delete().Top(10).WithTies();

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE TOP(@p0) WITH TIES", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Value()
        {
            IDelete delete = sql.Delete().Top(sql.Top(10));

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Top_Raw()
        {
            IDelete delete = sql.Delete().Top(sql.Raw("TOP({0})", 10));

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE TOP(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10
            }, result.Parameters);
        }

        [Fact]
        public void Invalid_Top_Percent()
        {
            IDelete delete = sql.Delete().Top(sql.Raw("TOP({0})", 10));

            Exception ex = Assert.Throws<InvalidOperationException>(() => ((IDeleteTop)delete).Percent());
            Assert.Equal("Top value must be a \"Suilder.Core.ITop\" instance.", ex.Message);
        }

        [Fact]
        public void Invalid_Top_WithTies()
        {
            IDelete delete = sql.Delete().Top(sql.Raw("TOP({0})", 10));

            Exception ex = Assert.Throws<InvalidOperationException>(() => ((IDeleteTop)delete).WithTies());
            Assert.Equal("Top value must be a \"Suilder.Core.ITop\" instance.", ex.Message);
        }

        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IDelete delete = sql.Delete()
                .Add(person)
                .Add(dept);

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE \"person\", \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IDelete delete = sql.Delete().Add(person, dept);

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE \"person\", \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IDelete delete = sql.Delete().Add(new List<IAlias> { person, dept });

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE \"person\", \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            Department dept = null;
            IDelete delete = sql.Delete()
                .Add(() => person)
                .Add(() => dept);

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE \"person\", \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            Department dept = null;
            IDelete delete = sql.Delete().Add(() => person, () => dept);

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE \"person\", \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            Department dept = null;
            IDelete delete = sql.Delete().Add(new List<Expression<Func<object>>> { () => person, () => dept });

            QueryResult result = engine.Compile(delete);

            Assert.Equal("DELETE \"person\", \"dept\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IDelete delete = sql.Delete().Top(10);

            Assert.Equal("DELETE TOP(10)", delete.ToString());
        }

        [Fact]
        public void To_String_With_Values()
        {
            IAlias person = sql.Alias("person");
            IAlias dept = sql.Alias("dept");
            IDelete delete = sql.Delete()
                .Add(person)
                .Add(dept);

            Assert.Equal("DELETE person, dept", delete.ToString());
        }
    }
}