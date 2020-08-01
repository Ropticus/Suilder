using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;
using Xunit;

namespace Suilder.Test.Builder
{
    public class QueryResultTest : BuilderBaseTest
    {
        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(sql.Or.Add(person["Id"].Eq(1), person["Name"].Like("SomeName".ToLikeAny())));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".* FROM \"person\" WHERE \"person\".\"Id\" = @p0 OR \"person\".\"Name\" "
                + "LIKE @p1; [@p0, 1], [@p1, %SomeName%]", result.ToString());
        }

        [Fact]
        public void To_String_Parameters_List()
        {
            engine.Options.ParameterPrefix = "?";
            engine.Options.ParameterIndex = false;

            IAlias person = sql.Alias("person");
            IQuery query = sql.Query
                .Select(person.All)
                .From(person)
                .Where(sql.Or.Add(person["Id"].Eq(1), person["Name"].Like("SomeName".ToLikeAny())));

            QueryResult result = engine.Compile(query);

            Assert.Equal("SELECT \"person\".* FROM \"person\" WHERE \"person\".\"Id\" = ? OR \"person\".\"Name\" "
                + "LIKE ?; 1, %SomeName%", result.ToString());
        }
    }
}