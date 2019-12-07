using System.Collections.Generic;
using Suilder.Builder;
using Suilder.Core;
using Xunit;

namespace Suilder.Test.Builder.Select
{
    public class OverTest : BuilderBaseTest
    {
        [Fact]
        public void Over()
        {
            IOver over = sql.Over;

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER()", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Over_PartitionBy()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over.PartitionBy(x => x.Add(person["DepartmentId"]));

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER(PARTITION BY \"person\".\"DepartmentId\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Over_PartitionBy_Value()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over.PartitionBy(sql.ValList.Add(person["DepartmentId"]));

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER(PARTITION BY \"person\".\"DepartmentId\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Over_PartitionBy_Raw()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over.PartitionBy(sql.Raw("{0}", person["DepartmentId"]));

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER(PARTITION BY \"person\".\"DepartmentId\")", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void Over_PartitionBy_OrderBy()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over
                .PartitionBy(x => x.Add(person["DepartmentId"]))
                .OrderBy(x => x.Add(person["Name"]).Asc);

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER(PARTITION BY \"person\".\"DepartmentId\" ORDER BY \"person\".\"Name\" ASC)", result.Sql);
        }

        [Fact]
        public void Over_PartitionBy_OrderBy_Value()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over
                .PartitionBy(x => x.Add(person["DepartmentId"]))
                .OrderBy(sql.OrderBy().Add(person["Name"]).Asc);

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER(PARTITION BY \"person\".\"DepartmentId\" ORDER BY \"person\".\"Name\" ASC)", result.Sql);
        }

        [Fact]
        public void Over_PartitionBy_OrderBy_Raw()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over
                .PartitionBy(x => x.Add(person["DepartmentId"]))
                .OrderBy(sql.Raw("ORDER BY {0} ASC", person["Name"]));

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER(PARTITION BY \"person\".\"DepartmentId\" ORDER BY \"person\".\"Name\" ASC)", result.Sql);
        }

        [Fact]
        public void Over_PartitionBy_OrderBy_Range()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over
                .PartitionBy(x => x.Add(person["DepartmentId"]))
                .OrderBy(x => x.Add(person["Name"]).Asc)
                .Range(sql.Raw("ROWS 5 PRECEDING"));

            QueryResult result = engine.Compile(over);

            Assert.Equal("OVER(PARTITION BY \"person\".\"DepartmentId\" ORDER BY \"person\".\"Name\" ASC ROWS 5 PRECEDING)",
                result.Sql);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOver over = sql.Over
                .PartitionBy(x => x.Add(person["DepartmentId"]))
                .OrderBy(x => x.Add(person["Name"]).Asc);

            Assert.Equal("OVER(PARTITION BY person.DepartmentId ORDER BY person.Name ASC)", over.ToString());
        }
    }
}