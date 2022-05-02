using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder
{
    public class QueryBuilderTest : BuilderBaseTest
    {
        [Fact]
        public void Write()
        {
            QueryResult result = new QueryBuilder(engine).Write("SELECT").ToQueryResult();

            Assert.Equal("SELECT", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void WriteFragment()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            QueryResult result;

            result = new QueryBuilder(engine).WriteFragment(from).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(op).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(query).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void WriteFragment_Parentheses_False()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            QueryResult result;

            result = new QueryBuilder(engine).WriteFragment(from, false).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(op, false).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(query, false).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void WriteFragment_Parentheses_True()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            QueryResult result;

            result = new QueryBuilder(engine).WriteFragment(from, true).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(op, true).ToQueryResult();

            Assert.Equal("(\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(query, true).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void WriteFragment_Parentheses_Never()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            QueryResult result;

            result = new QueryBuilder(engine).WriteFragment(from, Parentheses.Never).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(op, Parentheses.Never).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(query, Parentheses.Never).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void WriteFragment_Parentheses_SubFragment()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            QueryResult result;

            result = new QueryBuilder(engine).WriteFragment(from, Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(op, Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("(\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(query, Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void WriteFragment_Parentheses_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            QueryResult result;

            result = new QueryBuilder(engine).WriteFragment(from, Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(op, Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(query, Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void WriteFragment_Parentheses_Always()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);

            QueryResult result;

            result = new QueryBuilder(engine).WriteFragment(from, Parentheses.Always).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(op, Parentheses.Always).ToQueryResult();

            Assert.Equal("(\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteFragment(query, Parentheses.Always).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Invalid_WriteFragment_Parentheses()
        {
            IFrom from = sql.From("person");

            Exception ex = Assert.Throws<ArgumentException>(() =>
                new QueryBuilder(engine).WriteFragment(from, (Parentheses)int.MaxValue));
            Assert.Equal("Invalid value. (Parameter 'parentheses')", ex.Message);
        }

        [Fact]
        public void WriteValue()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            QueryResult result;

            result = new QueryBuilder(engine).WriteValue(from).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(op).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteValue(query).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(value).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void WriteValue_Parentheses_False()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            QueryResult result;

            result = new QueryBuilder(engine).WriteValue(from, false).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(op, false).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteValue(query, false).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(value, false).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void WriteValue_Parentheses_True()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            QueryResult result;

            result = new QueryBuilder(engine).WriteValue(from, true).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(op, true).ToQueryResult();

            Assert.Equal("(\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteValue(query, true).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(value, true).ToQueryResult();

            Assert.Equal("(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void WriteValue_Parentheses_Never()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            QueryResult result;

            result = new QueryBuilder(engine).WriteValue(from, Parentheses.Never).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(op, Parentheses.Never).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteValue(query, Parentheses.Never).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(value, Parentheses.Never).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void WriteValue_Parentheses_SubFragment()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            QueryResult result;

            result = new QueryBuilder(engine).WriteValue(from, Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(op, Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("(\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteValue(query, Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(value, Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void WriteValue_Parentheses_SubQuery()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            QueryResult result;

            result = new QueryBuilder(engine).WriteValue(from, Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("FROM \"person\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(op, Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteValue(query, Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(value, Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void WriteValue_Parentheses_Always()
        {
            IAlias person = sql.Alias("person");
            IFrom from = sql.From(person);
            IOperator op = sql.Eq(person["Id"], 1);
            IQuery query = sql.Query.From(from);
            string value = "abcd";

            QueryResult result;

            result = new QueryBuilder(engine).WriteValue(from, Parentheses.Always).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(op, Parentheses.Always).ToQueryResult();

            Assert.Equal("(\"person\".\"Id\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);

            result = new QueryBuilder(engine).WriteValue(query, Parentheses.Always).ToQueryResult();

            Assert.Equal("(FROM \"person\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);

            result = new QueryBuilder(engine).WriteValue(value, Parentheses.Always).ToQueryResult();

            Assert.Equal("(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = value
            }, result.Parameters);
        }

        [Fact]
        public void Invalid_WriteValue_Parentheses()
        {
            IFrom from = sql.From("person");

            Exception ex = Assert.Throws<ArgumentException>(() =>
                new QueryBuilder(engine).WriteValue(from, (Parentheses)int.MaxValue));
            Assert.Equal("Invalid value. (Parameter 'parentheses')", ex.Message);
        }

        [Fact]
        public void AddParameter()
        {
            QueryResult result = new QueryBuilder(engine).AddParameter("abcd").ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void AddParameter_Parentheses_False()
        {
            QueryResult result = new QueryBuilder(engine).AddParameter("abcd", false).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void AddParameter_Parentheses_True()
        {
            QueryResult result = new QueryBuilder(engine).AddParameter("abcd", true).ToQueryResult();

            Assert.Equal("(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void AddParameter_Parentheses_Never()
        {
            QueryResult result = new QueryBuilder(engine).AddParameter("abcd", Parentheses.Never).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void AddParameter_Parentheses_SubFragment()
        {
            QueryResult result = new QueryBuilder(engine).AddParameter("abcd", Parentheses.SubFragment).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void AddParameter_Parentheses_SubQuery()
        {
            QueryResult result = new QueryBuilder(engine).AddParameter("abcd", Parentheses.SubQuery).ToQueryResult();

            Assert.Equal("@p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void AddParameter_Parentheses_Always()
        {
            QueryResult result = new QueryBuilder(engine).AddParameter("abcd", Parentheses.Always).ToQueryResult();

            Assert.Equal("(@p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Invalid_AddParameter_Parentheses()
        {
            Exception ex = Assert.Throws<ArgumentException>(() =>
                new QueryBuilder(engine).AddParameter("abcd", (Parentheses)int.MaxValue));
            Assert.Equal("Invalid value. (Parameter 'parentheses')", ex.Message);
        }
    }
}