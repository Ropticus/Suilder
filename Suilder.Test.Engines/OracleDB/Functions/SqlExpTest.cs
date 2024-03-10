using System;
using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Test.Engines.Tables;
using Xunit;

namespace Suilder.Test.Engines.OracleDB.Functions
{
    public class SqlExpTest : OracleDBBaseTest
    {
        [Fact]
        public void Abs()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Abs(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("ABS(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Avg()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Avg(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void AvgDistinct()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.AvgDistinct(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("AVG(DISTINCT \"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Cast()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Cast(person.Salary, sql.Type("VARCHAR")));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CAST(\"PERSON\".\"SALARY\" AS VARCHAR)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Ceiling()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Ceiling(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("CEIL(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Coalesce()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Coalesce(person.Name, person.Surname));

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"PERSON\".\"NAME\", \"PERSON\".\"SURNAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Coalesce_Object_Overload()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Coalesce(person.Name, person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("COALESCE(\"PERSON\".\"NAME\", \"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Concat()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Concat(person.Name, person.Surname));

            QueryResult result = engine.Compile(func);

            Assert.Equal("(\"PERSON\".\"NAME\" || \"PERSON\".\"SURNAME\")", result.Sql);
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

            Assert.Equal("COUNT(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Count_Distinct()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.CountDistinct(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("COUNT(DISTINCT \"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Floor()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Floor(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("FLOOR(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LastInsertId()
        {
            IFunction func = (IFunction)sql.Val(() => SqlExp.LastInsertId());

            Exception ex = Assert.Throws<ClauseNotSupportedException>(() => engine.Compile(func));
            Assert.Equal("Function \"LASTINSERTID\" is not supported in this engine.", ex.Message);
        }

        [Fact]
        public void Length()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Length(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("LENGTH(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Lower()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Lower(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("LOWER(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.LTrim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("LTRIM(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LTrim_Character()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.LTrim(person.Name, ","));

            QueryResult result = engine.Compile(func);

            Assert.Equal("LTRIM(\"PERSON\".\"NAME\", :p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":p0"] = ","
            }, result.Parameters);
        }


        [Fact]
        public void Max()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Max(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("MAX(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Min()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Min(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("MIN(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Now()
        {
            IFunction func = (IFunction)sql.Val(() => SqlExp.Now());

            QueryResult result = engine.Compile(func);

            Assert.Equal("SYSDATE", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void NullIf()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.NullIf(person.Name, "empty"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("NULLIF(\"PERSON\".\"NAME\", :p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":p0"] = "empty"
            }, result.Parameters);
        }

        [Fact]
        public void Replace()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Replace(person.Name, "a", "b"));

            QueryResult result = engine.Compile(func);

            Assert.Equal("REPLACE(\"PERSON\".\"NAME\", :p0, :p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":p0"] = "a",
                [":p1"] = "b"
            }, result.Parameters);
        }

        [Fact]
        public void Round()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Round(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Round_Places()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Round(person.Salary, 2));

            QueryResult result = engine.Compile(func);

            Assert.Equal("ROUND(\"PERSON\".\"SALARY\", :p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":p0"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void RTrim()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.RTrim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void RTrim_Character()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.RTrim(person.Name, ","));

            QueryResult result = engine.Compile(func);

            Assert.Equal("RTRIM(\"PERSON\".\"NAME\", :p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":p0"] = ","
            }, result.Parameters);
        }

        [Fact]
        public void Substring()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Substring(person.Name, 2, 4));

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUBSTR(\"PERSON\".\"NAME\", :p0, :p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":p0"] = 2,
                [":p1"] = 4
            }, result.Parameters);
        }

        [Fact]
        public void Sum()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Sum(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(\"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void SumDistinct()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.SumDistinct(person.Salary));

            QueryResult result = engine.Compile(func);

            Assert.Equal("SUM(DISTINCT \"PERSON\".\"SALARY\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Trim(person.Name));

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(\"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Trim_Character()
        {
            Person person = null;
            IFunction func = (IFunction)sql.Val(() => SqlExp.Trim(person.Name, ","));

            QueryResult result = engine.Compile(func);

            Assert.Equal("TRIM(:p0 FROM \"PERSON\".\"NAME\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                [":p0"] = ","
            }, result.Parameters);
        }
    }
}