using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Xunit;

namespace Suilder.Test.Builder.Functions
{
    public class SqlFnTest : BuilderBaseTest
    {
        [Fact]
        public void Abs()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Abs(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("ABS(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Avg()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Avg(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void AvgDistinct()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.AvgDistinct(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(DISTINCT \"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cast()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Cast(person["Salary"], sql.Type("VARCHAR"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CAST(\"person\".\"Salary\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Ceiling()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Ceiling(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CEILING(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Coalesce()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Coalesce(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Concat()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Concat(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Concat_With_Or()
        {
            engine.AddFunction(FunctionName.Concat, FunctionHelper.ConcatOr);

            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Concat(person["Name"], person["SurName"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("(\"person\".\"Name\" || \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Concat_With_Or_SubOperator()
        {
            engine.AddFunction(FunctionName.Concat, FunctionHelper.ConcatOr);

            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT").Add(sql.Gt(person["Id"], 10), sql.Lt(person["Id"], 20));

            QueryResult result = engine.Compile(func);

            Assert.Equal("((\"person\".\"Id\" > @p0) || (\"person\".\"Id\" < @p1))", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Concat_With_Or_SubOperator_List()
        {
            engine.AddFunction(FunctionName.Concat, FunctionHelper.ConcatOr);

            IAlias person = sql.Alias("person");
            IFunction func = sql.Function("CONCAT").Add(sql.Add.Add(person["Salary"], 1000m),
                sql.Multiply.Add(person["Salary"], 2m));

            QueryResult result = engine.Compile(func);

            Assert.Equal("((\"person\".\"Salary\" + @p0) || (\"person\".\"Salary\" * @p1))", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1000m,
                ["@p1"] = 2m
            }, result.Parameters);
        }

        [Fact]
        public void Concat_With_Or_SubQuery()
        {
            engine.AddFunction(FunctionName.Concat, FunctionHelper.ConcatOr);

            IFunction func = sql.Function("CONCAT").Add(sql.RawQuery("Subquery1"), sql.RawQuery("Subquery2"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("((Subquery1) || (Subquery2))", result.Sql);
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

            Assert.Equal("COUNT(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_Distinct()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.CountDistinct(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(DISTINCT \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Floor()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Floor(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("FLOOR(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LastInsertId()
        {
            IFunction func = SqlFn.LastInsertId();

            QueryResult result = engine.Compile(func);

            Assert.Equal("LASTINSERTID()", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Length()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Length(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("LENGTH(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Lower()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Lower(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("LOWER(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.LTrim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("LTRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim_Character()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.LTrim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("LTRIM(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void LTrim_Trim_Leading()
        {
            engine.AddFunction(FunctionName.LTrim, FunctionHelper.TrimLeading);

            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.LTrim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(LEADING FROM \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim_Character_Trim_Leading()
        {
            engine.AddFunction(FunctionName.LTrim, FunctionHelper.TrimLeading);

            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.LTrim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(LEADING @p0 FROM \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void Max()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Max(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("MAX(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Min()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Min(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("MIN(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Now()
        {
            IFunction func = SqlFn.Now();

            QueryResult result = engine.Compile(func);

            Assert.Equal("NOW()", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void NullIf()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.NullIf(person["Name"], "empty");

            QueryResult result = engine.Compile(func);

            Assert.Equal("NULLIF(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "empty"
            }, result.Parameters);
        }

        [Fact]
        public void Replace()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Replace(person["Name"], "a", "b");

            QueryResult result = engine.Compile(func);

            Assert.Equal("REPLACE(\"person\".\"Name\", @p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "a",
                ["@p1"] = "b"
            }, result.Parameters);
        }

        [Fact]
        public void Round()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Round(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Round_Places()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Round(person["Salary"], 2m);

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"person\".\"Salary\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2m
            }, result.Parameters);
        }

        [Fact]
        public void RTrim()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.RTrim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void RTrim_Character()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.RTrim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void RTrim_Trim_Trailing()
        {
            engine.AddFunction(FunctionName.RTrim, FunctionHelper.TrimTrailing);

            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.RTrim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(TRAILING FROM \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void RTrim_Character_Trim_Trailing()
        {
            engine.AddFunction(FunctionName.RTrim, FunctionHelper.TrimTrailing);

            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.RTrim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(TRAILING @p0 FROM \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void Substring()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Substring(person["Name"], 2, 4);

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUBSTRING(\"person\".\"Name\", @p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2,
                ["@p1"] = 4
            }, result.Parameters);
        }

        [Fact]
        public void Sum()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Sum(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SumDistinct()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.SumDistinct(person["Salary"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(DISTINCT \"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Trim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim_Character()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Trim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void Trim_Both()
        {
            engine.AddFunction(FunctionName.Trim, FunctionHelper.TrimBoth);

            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Trim(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim_Character_Both()
        {
            engine.AddFunction(FunctionName.Trim, FunctionHelper.TrimBoth);

            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Trim(person["Name"], ",");

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(@p0 FROM \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void Upper()
        {
            IAlias person = sql.Alias("person");
            IFunction func = SqlFn.Upper(person["Name"]);

            QueryResult result = engine.Compile(func);

            Assert.Equal("UPPER(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}