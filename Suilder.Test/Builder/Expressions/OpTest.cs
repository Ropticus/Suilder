using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Expressions
{
    public class OpTest : BuilderBaseTest
    {
        [Fact]
        public void Lambda_Overload()
        {
            Person person = null;
            Expression<Func<bool>> expression = () => person.Id == 1;
            IOperator op = sql.Op((LambdaExpression)expression);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }

        [Fact]
        public void Body_Overload()
        {
            Person person = null;
            Expression<Func<bool>> expression = () => person.Id == 1;
            IOperator op = sql.Op(expression.Body);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1
            }, result.Parameters);
        }
    }
}