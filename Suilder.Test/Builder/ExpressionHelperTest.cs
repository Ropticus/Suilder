using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder
{
    public class ExpressionHelperTest : BuilderBaseTest
    {
        [Fact]
        public void Function_Static()
        {
            Expression<Func<object>> expression = () => String.Concat("Value1", "Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Function(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "Value2"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Instance()
        {
            Expression<Func<object>> expression = () => new Custom("Value1").Concat("Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Function(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("Value1"),
                ["@p1"] = "Value2"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Params_Static()
        {
            Expression<Func<object>> expression = () => String.Concat(new string[] { "Value1", "Value2", "Value3" });
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionParams(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "Value2",
                ["@p2"] = "Value3"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Params_Instance()
        {
            Expression<Func<object>> expression = () => new Custom("Value1").Concat("Value2", "Value3");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionParams(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("Value1"),
                ["@p1"] = "Value2",
                ["@p2"] = "Value3"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Params_Multiple()
        {
            Expression<Func<object>> expression = () => new Custom("Value1").Concat('a', "Value2", "Value3");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionParams(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1, @p2, @p3)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("Value1"),
                ["@p1"] = 'a',
                ["@p2"] = "Value2",
                ["@p3"] = "Value3"
            }, result.Parameters);
        }

        private static object FunctionWithName(string name, params object[] values) => null;

        [Fact]
        public void Function_With_Name()
        {
            Expression<Func<object>> expression = () => FunctionWithName("CONCAT", "Value1", "Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionWithName(methodExpression));

            Assert.Equal("CONCAT(@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "Value2"
            }, result.Parameters);
        }

        private static object FunctionWithName() => null;

        [Fact]
        public void Function_With_Name_Without_Parameters()
        {
            Expression<Func<object>> expression = () => FunctionWithName();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.FunctionWithName(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Function_With_Name_Invalid_Name()
        {
            Person person = null;
            Expression<Func<object>> expression = () => FunctionWithName(person.Name, "Value1", "Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.FunctionWithName(methodExpression));
            Assert.Equal("Invalid expression, the name must be a constant.", ex.Message);
        }

        [Fact]
        public void Operator_Static()
        {
            Expression<Func<bool>> expression = () => String.Equals("Value1", "Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Operator(methodExpression, sql.Eq));

            Assert.Equal("@p0 = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "Value2"
            }, result.Parameters);
        }

        [Fact]
        public void Operator_Static_Invalid()
        {
            Expression<Func<int>> expression = () => String.Compare("Value1", "Value2", true);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Operator(methodExpression, sql.Eq));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Operator_Instance()
        {
            Expression<Func<bool>> expression = () => "Value1".Equals("Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Operator(methodExpression, sql.Eq));

            Assert.Equal("@p0 = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "Value2"
            }, result.Parameters);
        }

        [Fact]
        public void Operator_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "Value1".Equals("Value2", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Operator(methodExpression, sql.Eq));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Single_Operator_Static()
        {
            Expression<Func<bool>> expression = () => String.IsNullOrEmpty("Value1");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.SingleOperator(methodExpression, sql.IsNull));

            Assert.Equal("@p0 IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1"
            }, result.Parameters);
        }

        [Fact]
        public void Single_Operator_Static_Invalid()
        {
            Expression<Func<bool>> expression = () => String.Equals("Value1", null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ExpressionHelper.SingleOperator(methodExpression, sql.IsNull));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Single_Operator_Instance()
        {
            Expression<Func<bool>> expression = () => new Custom("Value1").IsNull();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.SingleOperator(methodExpression, sql.IsNull));

            Assert.Equal("@p0 IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("Value1")
            }, result.Parameters);
        }

        [Fact]
        public void Single_Operator_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "Value1".Equals(null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ExpressionHelper.SingleOperator(methodExpression, sql.IsNull));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Not_Operator_Static()
        {
            Person person = null;
            Expression<Func<bool>> expression = () => Custom.Not(person.Name == "Value1");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Not(methodExpression));

            Assert.Equal("NOT \"person\".\"Name\" = @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1"
            }, result.Parameters);
        }

        [Fact]
        public void Not_Operator_Static_Invalid()
        {
            Expression<Func<bool>> expression = () => String.Equals("Value1", null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Not(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Not_Operator_Instance()
        {
            Expression<Func<bool>> expression = () => new Custom("Value1").Not();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Not(methodExpression));

            Assert.Equal("NOT @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("Value1")
            }, result.Parameters);
        }

        [Fact]
        public void Not_Operator_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "Value1".Equals(null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Not(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Any_Static()
        {
            Expression<Func<int>> expression = () => String.Compare("Value1", "Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeAny(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "%Value2%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Any_Static_Invalid()
        {
            Expression<Func<int>> expression = () => String.Compare("Value1", "Value2", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeAny(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Any_Instance()
        {
            Expression<Func<bool>> expression = () => "Value1".Contains("Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeAny(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "%Value2%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Any_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "Value1".Contains("Value2", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeAny(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Start_Static()
        {
            Expression<Func<int>> expression = () => String.Compare("Value1", "Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeStart(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "Value2%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Start_Static_Invalid()
        {
            Expression<Func<int>> expression = () => String.Compare("Value1", "Value2", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeStart(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Start_Instance()
        {
            Expression<Func<bool>> expression = () => "Value1".Contains("Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeStart(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "Value2%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Start_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "Value1".Contains("Value2", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeStart(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_End_Static()
        {
            Expression<Func<int>> expression = () => String.Compare("Value1", "Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeEnd(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "%Value2"
            }, result.Parameters);
        }

        [Fact]
        public void Like_End_Static_Invalid()
        {
            Expression<Func<int>> expression = () => String.Compare("Value1", "Value2", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeEnd(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_End_Instance()
        {
            Expression<Func<bool>> expression = () => "Value1".Contains("Value2");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeEnd(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "Value1",
                ["@p1"] = "%Value2"
            }, result.Parameters);
        }

        [Fact]
        public void Like_End_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "Value1".Contains("Value2", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeEnd(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Val_Static()
        {
            Person person = new Person()
            {
                Name = "Value1"
            };
            Expression<Func<object>> expression = () => Custom.Val(person.Name);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Assert.Equal("Value1", ExpressionHelper.Val(methodExpression));
        }

        [Fact]
        public void Val_Static_Invalid()
        {
            Expression<Func<bool>> expression = () => String.Equals("Value1", null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Val(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Val_Instance()
        {
            Expression<Func<object>> expression = () => new Custom("Value1").Val();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Assert.Equal(new Custom("Value1"), ExpressionHelper.Val(methodExpression));
        }

        [Fact]
        public void Val_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "Value1".Equals(null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Val(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        private class Custom : IEquatable<Custom>
        {
            public Custom(string value) => Value = value;

            private string Value { get; set; }

            public bool Equals(Custom other) => other.Value == Value;

            public override int GetHashCode() => Value.GetHashCode();

            public override string ToString() => Value;

            public Custom Concat(string value1) => null;

            public Custom Concat(params string[] values) => null;

            public Custom Concat(char value1, params string[] values) => null;

            public bool IsNull() => false;

            public bool Not() => false;

            public static bool Not(bool value) => false;

            public Custom Val() => null;

            public static T Val<T>(T value) => value;
        }
    }
}