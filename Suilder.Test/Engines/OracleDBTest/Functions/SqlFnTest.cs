using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Engines.OracleDBTest.Functions
{
    public class SqlFnTest : OracleDBBaseTest
    {
        [Fact]
        public void Abs()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Abs(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("ABS(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Avg()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Avg(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void AvgDistinct()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.AvgDistinct(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(DISTINCT \"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cast()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Cast(person["Salary"], sql.Type("VARCHAR"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CAST(\"PERSON\".\"SALARY\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Ceiling()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Ceiling(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CEIL(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Coalesce()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Coalesce(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"PERSON\".\"NAME\", \"PERSON\".\"SURNAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Concat()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Concat(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("\"PERSON\".\"NAME\" || \"PERSON\".\"SURNAME\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count()
        {
            IFunction func = SqlFn.Count();

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(*)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_Column()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Count(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_Distinct()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.CountDistinct(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(DISTINCT \"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Floor()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Floor(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("FLOOR(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LastInsertId()
        {
            IFunction func = SqlFn.LastInsertId();

            Exception ex = Assert.Throws<ClauseNotSupportedException>(() => engine.Compile(func));
            Assert.Equal($"Function \"LASTINSERTID\" is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void Length()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Length(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("LENGTH(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Lower()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Lower(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("LOWER(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.LTrim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("LTRIM(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim_Character()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.LTrim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("LTRIM(\"PERSON\".\"NAME\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "," }, result.Parameters);
        }

        [Fact]
        public void Max()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Max(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("MAX(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Min()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Min(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("MIN(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Now()
        {
            IFunction func = SqlFn.Now();

            QueryResult result = engine.Compile(func);

            Assert.Equal("SYSDATE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void NullIf()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.NullIf(person["Name"], "empty");

            QueryResult result = engine.Compile(func);

            Assert.Equal("NULLIF(\"PERSON\".\"NAME\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "empty" }, result.Parameters);
        }

        [Fact]
        public void Replace()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Replace(person["Name"], "a", "b");

            QueryResult result = engine.Compile(func);

            Assert.Equal("REPLACE(\"PERSON\".\"NAME\", @p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "a", ["@p1"] = "b" }, result.Parameters);
        }

        [Fact]
        public void Round()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Round(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Round_Places()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Round(person["Salary"], 2m);

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"PERSON\".\"SALARY\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 2m }, result.Parameters);
        }

        [Fact]
        public void RTrim()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.RTrim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void RTrim_Character()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.RTrim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"PERSON\".\"NAME\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "," }, result.Parameters);
        }

        [Fact]
        public void Substring()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Substring(person["Name"], 2, 4);

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUBSTR(\"PERSON\".\"NAME\", @p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = 2, ["@p1"] = 4 }, result.Parameters);
        }

        [Fact]
        public void Sum()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Sum(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SumDistinct()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.SumDistinct(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(DISTINCT \"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Trim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim_Character()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Trim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(@p0 FROM \"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>() { ["@p0"] = "," }, result.Parameters);
        }
    }
}