using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class CaseTest : BaseTest
    {
        [Fact]
        public void When()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case
                .When(person["Name"].IsNotNull(), person["Name"]);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Else()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case
                .When(person["Name"].IsNotNull(), person["Name"])
                .Else(person["SurName"]);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" "
                + "ELSE \"person\".\"SurName\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Multiple()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case
                .When(person["Salary"].Gt(2000m), "Greather than 2000")
                .When(person["Salary"].Gt(1000m), "Greather than 1000")
                .Else("Under 1000");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Salary\" > @p0 THEN @p1 "
                + "WHEN \"person\".\"Salary\" > @p2 THEN @p3 ELSE @p4 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>()
            {
                ["@p0"] = 2000m,
                ["@p1"] = "Greather than 2000",
                ["@p2"] = 1000m,
                ["@p3"] = "Greather than 1000",
                ["@p4"] = "Under 1000"
            }, result.Parameters);
        }

        [Fact]
        public void When_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case
                .When(() => person.Name != null, () => person.Name);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Else_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case
                .When(() => person.Name != null, () => person.Name)
                .Else(() => person.SurName);

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Name\" IS NOT NULL THEN \"person\".\"Name\" "
                + "ELSE \"person\".\"SurName\" END", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void When_Multiple_Expression()
        {
            Person person = null;
            ICase caseWhen = sql.Case
                .When(() => person.Salary > 2000m, "Greather than 2000")
                .When(() => person.Salary > 1000m, "Greather than 1000")
                .Else("Under 1000");

            QueryResult result = engine.Compile(caseWhen);

            Assert.Equal("CASE WHEN \"person\".\"Salary\" > @p0 THEN @p1 "
                + "WHEN \"person\".\"Salary\" > @p2 THEN @p3 ELSE @p4 END", result.Sql);
            Assert.Equal(new Dictionary<string, object>()
            {
                ["@p0"] = 2000m,
                ["@p1"] = "Greather than 2000",
                ["@p2"] = 1000m,
                ["@p3"] = "Greather than 1000",
                ["@p4"] = "Under 1000"
            }, result.Parameters);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            ICase caseWhen = sql.Case
                .When(person["Name"].IsNotNull(), person["Name"])
                .Else(person["SurName"]);

            Assert.Equal("CASE WHEN person.Name IS NOT NULL THEN person.Name ELSE person.SurName", caseWhen.ToString());
        }
    }
}