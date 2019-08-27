using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Functions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Functions
{
    public class SqlExpTest : BuilderBaseTest
    {
        [Fact]
        public void Function()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("CONCAT", person.Name, person.SurName));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Function_Name_Only()
        {
            IFunction func = (IFunction)sql.Val(() => SqlExp.Function("NOW"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("NOW()", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Val_Alias()
        {
            Person person = null;
            Person personValue = new Person()
            {
                Id = 1
            };
            IOperator func = (IOperator)sql.Op(() => person.Id == SqlExp.Val(personValue.Id));

            QueryResult result = engine.Compile(func);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = personValue.Id
            }, result.Parameters);
        }

        [Fact]
        public void Val_Method()
        {
            Person person = null;
            Person personValue = new Person()
            {
                Name = "SomeName"
            };
            IOperator func = (IOperator)sql.Op(() => person.Active == SqlExp.Val(personValue.Name.Contains("Name")));

            QueryResult result = engine.Compile(func);

            Assert.Equal("\"person\".\"Active\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = personValue.Name.Contains("Name")
            }, result.Parameters);
        }

        [Fact]
        public void Abs()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Abs(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("ABS(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Avg()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Avg(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void AvgDistinct()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.AvgDistinct(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(DISTINCT \"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cast()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Cast(person.Salary, sql.Type("VARCHAR")));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CAST(\"person\".\"Salary\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Ceiling()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Ceiling(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CEILING(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Coalesce()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Coalesce(person.Name, person.SurName));

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Coalesce_Object_Overload()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Coalesce(person.Name, person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"person\".\"Name\", \"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Concat()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Concat(person.Name, person.SurName));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CONCAT(\"person\".\"Name\", \"person\".\"SurName\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Concat_With_Or()
        {
            engine.AddFunction(FunctionName.Concat, FunctionHelper.ConcatOr);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Concat(person.Name, person.SurName));

            QueryResult result = engine.Compile(func);

            Assert.Equal("\"person\".\"Name\" || \"person\".\"SurName\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count()
        {
            IFunction func = (IFunction)sql.Val(() => SqlExp.Count());

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(*)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_Column()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Count(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_Distinct()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.CountDistinct(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(DISTINCT \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Floor()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Floor(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("FLOOR(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LastInsertId()
        {
            IFunction func = (IFunction)sql.Val(() => SqlExp.LastInsertId());

            QueryResult result = engine.Compile(func);

            Assert.Equal("LASTINSERTID()", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Length()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Length(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("LENGTH(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Lower()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Lower(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("LOWER(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.LTrim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("LTRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim_Character()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.LTrim(person.Name, ","));

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

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.LTrim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(LEADING FROM \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim_Character_Trim_Leading()
        {
            engine.AddFunction(FunctionName.LTrim, FunctionHelper.TrimLeading);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.LTrim(person.Name, ","));

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
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Max(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("MAX(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Min()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Min(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("MIN(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Now()
        {
            IFunction func = (IFunction)sql.Val(() => SqlExp.Now());

            QueryResult result = engine.Compile(func);

            Assert.Equal("NOW()", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void NullIf()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.NullIf(person.Name, "empty"));

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
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Replace(person.Name, "a", "b"));

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
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Round(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Round_Places()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Round(person.Salary, 2));

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"person\".\"Salary\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void RTrim()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.RTrim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void RTrim_Character()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.RTrim(person.Name, ","));

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"person\".\"Name\", @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void RTrim_Trim_Leading()
        {
            engine.AddFunction(FunctionName.RTrim, FunctionHelper.TrimTrailing);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.RTrim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(TRAILING FROM \"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void RTrim_Character_Trim_Leading()
        {
            engine.AddFunction(FunctionName.RTrim, FunctionHelper.TrimTrailing);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.RTrim(person.Name, ","));

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
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Substring(person.Name, 2, 4));

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
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Sum(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(\"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SumDistinct()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.SumDistinct(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(DISTINCT \"person\".\"Salary\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Trim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim_Character()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Trim(person.Name, ","));

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

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Trim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim_Character_Both()
        {
            engine.AddFunction(FunctionName.Trim, FunctionHelper.TrimBoth);

            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Trim(person.Name, ","));

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
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Upper(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("UPPER(\"person\".\"Name\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Add_Remove_Function()
        {
            ExpressionProcessor.AddFunction(typeof(string), nameof(string.Trim));
            Assert.True(ExpressionProcessor.ContainsFunction(typeof(string), nameof(string.Trim)));

            ExpressionProcessor.RemoveFunction(typeof(string), nameof(string.Trim));
            Assert.False(ExpressionProcessor.ContainsFunction(typeof(string), nameof(string.Trim)));
        }
    }
}