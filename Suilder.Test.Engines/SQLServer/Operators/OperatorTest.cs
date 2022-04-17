using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Engines.Tables;
using Xunit;

namespace Suilder.Test.Engines.SQLServer.Operators
{
    public class OperatorTest : SQLServerBaseTest
    {
        [Fact]
        public void Eq()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Eq(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void NotEq()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotEq(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] <> @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Like()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Like(person["Name"], "%abcd%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Name] LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void NotLike()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotLike(person["Name"], "%abcd%");

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Name] NOT LIKE @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Lt()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Lt(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] < @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Le()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Le(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] <= @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Gt()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Gt(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] > @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Ge()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Ge(person["Id"], 1);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] >= @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void In()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.In(person["Id"], new int[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Fact]
        public void NotIn()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotIn(person["Id"], new int[] { 1, 2, 3 });

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] NOT IN (@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2,
                ["@p2"] = 3
            }, result.Parameters);
        }

        [Fact]
        public void Not()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Not(person["Id"].Eq(1));

            QueryResult result = engine.Compile(op);

            Assert.Equal("NOT ([person].[Id] = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void IsNull()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNull(person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Name] IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void IsNotNull()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.IsNotNull(person["Name"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Name] IS NOT NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Between()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Between(person["Id"], 10, 20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void NotBetween()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.NotBetween(person["Id"], 10, 20);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] NOT BETWEEN @p0 AND @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 10,
                ["@p1"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void All()
        {
            IOperator op = sql.All(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("ALL (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Any()
        {
            IOperator op = sql.Any(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("ANY (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Exists()
        {
            IOperator op = sql.Exists(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("EXISTS (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Some()
        {
            IOperator op = sql.Some(sql.RawQuery("Subquery"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("SOME (Subquery)", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void And()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.And
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] = @p0 AND [person].[Active] = @p1 AND [person].[Name] LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Or()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Or
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%abcd%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Id] = @p0 OR [person].[Active] = @p1 OR [person].[Name] LIKE @p2",
                result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%abcd%"
            }, result.Parameters);
        }

        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Add
                .Add(person["Salary"])
                .Add(100m)
                .Add(200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Salary] + @p0 + @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200
            }, result.Parameters);
        }

        [Fact]
        public void Subtract()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Subtract
                .Add(person["Salary"])
                .Add(100m)
                .Add(200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Salary] - @p0 - @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200
            }, result.Parameters);
        }

        [Fact]
        public void Multiply()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m)
                .Add(200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Salary] * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200
            }, result.Parameters);
        }

        [Fact]
        public void Divide()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Divide
                .Add(person["Salary"])
                .Add(100m)
                .Add(200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Salary] / @p0 / @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200
            }, result.Parameters);
        }

        [Fact]
        public void Modulo()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Modulo
                .Add(person["Salary"])
                .Add(100m)
                .Add(200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Salary] % @p0 % @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200
            }, result.Parameters);
        }

        [Fact]
        public void Negate()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Negate(person["Salary"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("- [person].[Salary]", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void BitAnd()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitAnd
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Flags] & @p0 & @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void BitOr()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitOr
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Flags] | @p0 | @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void BitXor()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitXor
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Flags] ^ @p0 ^ @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void BitNot()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.BitNot(person["Flags"]);

            QueryResult result = engine.Compile(op);

            Assert.Equal("~ [person].[Flags]", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void LeftShift()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.LeftShift
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Flags] << @p0 << @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void RightShift()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.RightShift
                .Add(person["Flags"])
                .Add(1)
                .Add(2);

            QueryResult result = engine.Compile(op);

            Assert.Equal("[person].[Flags] >> @p0 >> @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = 2
            }, result.Parameters);
        }

        [Fact]
        public void Union()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT [person].[Name] FROM [Person] AS [person]) "
                + "UNION (SELECT [dept].[Name] FROM [Dept] AS [dept])", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Union_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Union(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT (SELECT [dept].[Name] FROM [Dept] AS [dept])) "
                + "UNION ((SELECT [person].[SurName] FROM [Person] AS [person]) "
                + "INTERSECT (SELECT [dept].[Name] FROM [Dept] AS [dept]))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void UnionAll()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT [person].[Name] FROM [Person] AS [person]) "
                + "UNION ALL (SELECT [dept].[Name] FROM [Dept] AS [dept])", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void UnionAll_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.UnionAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT (SELECT [dept].[Name] FROM [Dept] AS [dept])) "
                + "UNION ALL ((SELECT [person].[SurName] FROM [Person] AS [person]) "
                + "INTERSECT (SELECT [dept].[Name] FROM [Dept] AS [dept]))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Except()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Except(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT (SELECT [dept].[Name] FROM [Dept] AS [dept])", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Except_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Except(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT (SELECT [dept].[Name] FROM [Dept] AS [dept])) "
                + "EXCEPT ((SELECT [person].[SurName] FROM [Person] AS [person]) "
                + "INTERSECT (SELECT [dept].[Name] FROM [Dept] AS [dept]))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void ExceptAll()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.ExceptAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT ALL (SELECT [dept].[Name] FROM [Dept] AS [dept])", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void ExceptAll_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.ExceptAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT (SELECT [dept].[Name] FROM [Dept] AS [dept])) "
                + "EXCEPT ALL ((SELECT [person].[SurName] FROM [Person] AS [person]) "
                + "INTERSECT (SELECT [dept].[Name] FROM [Dept] AS [dept]))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Intersect()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Intersect(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT [person].[Name] FROM [Person] AS [person]) "
                + "INTERSECT (SELECT [dept].[Name] FROM [Dept] AS [dept])", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Intersect_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.Intersect(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT (SELECT [dept].[Name] FROM [Dept] AS [dept])) "
                + "INTERSECT ((SELECT [person].[SurName] FROM [Person] AS [person]) "
                + "INTERSECT (SELECT [dept].[Name] FROM [Dept] AS [dept]))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void IntersectAll()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Query.Select(() => person.Name).From(() => person),
                sql.Query.Select(() => dept.Name).From(() => dept));

            QueryResult result = engine.Compile(op);

            Assert.Equal("(SELECT [person].[Name] FROM [Person] AS [person]) "
                + "INTERSECT ALL (SELECT [dept].[Name] FROM [Dept] AS [dept])", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void IntersectAll_SubOperator()
        {
            Person person = null;
            Department dept = null;
            IOperator op = sql.IntersectAll(
                sql.Except(
                    sql.Query.Select(() => person.Name).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)),
                sql.Intersect(
                    sql.Query.Select(() => person.SurName).From(() => person),
                    sql.Query.Select(() => dept.Name).From(() => dept)));

            QueryResult result = engine.Compile(op);

            Assert.Equal("((SELECT [person].[Name] FROM [Person] AS [person]) "
                + "EXCEPT (SELECT [dept].[Name] FROM [Dept] AS [dept])) "
                + "INTERSECT ALL ((SELECT [person].[SurName] FROM [Person] AS [person]) "
                + "INTERSECT (SELECT [dept].[Name] FROM [Dept] AS [dept]))", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }
    }
}