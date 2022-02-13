using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.Expressions
{
    public class ExpressionHelperTest : BuilderBaseTest
    {
        [Fact]
        public void Function_Static()
        {
            Expression<Func<object>> expression = () => string.Concat("abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Function(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Instance()
        {
            Expression<Func<object>> expression = () => new Custom("abcd").Concat("efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Function(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("abcd"),
                ["@p1"] = "efgh"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Params_Static()
        {
            Expression<Func<object>> expression = () => string.Concat(new string[] { "abcd", "efgh", "ijkl" });
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionParams(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh",
                ["@p2"] = "ijkl"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Params_Instance()
        {
            Expression<Func<object>> expression = () => new Custom("abcd").Concat("efgh", "ijkl");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionParams(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1, @p2)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("abcd"),
                ["@p1"] = "efgh",
                ["@p2"] = "ijkl"
            }, result.Parameters);
        }

        [Fact]
        public void Function_Params_Multiple()
        {
            Expression<Func<object>> expression = () => new Custom("abcd").Concat('a', "efgh", "ijkl");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionParams(methodExpression, "CONCAT"));

            Assert.Equal("CONCAT(@p0, @p1, @p2, @p3)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("abcd"),
                ["@p1"] = 'a',
                ["@p2"] = "efgh",
                ["@p3"] = "ijkl"
            }, result.Parameters);
        }

        private static object FunctionWithName(string name, params object[] values) => null;

        [Fact]
        public void Function_With_Name()
        {
            Expression<Func<object>> expression = () => FunctionWithName("CONCAT", "abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.FunctionWithName(methodExpression));

            Assert.Equal("CONCAT(@p0, @p1)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh"
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
            Expression<Func<object>> expression = () => FunctionWithName(person.Name, "abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.FunctionWithName(methodExpression));
            Assert.Equal("Invalid expression, the name must be a constant.", ex.Message);
        }

        [Fact]
        public void Unary_Operator_Static()
        {
            Expression<Func<bool>> expression = () => string.IsNullOrEmpty("abcd");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.UnaryOperator(methodExpression, sql.IsNull));

            Assert.Equal("@p0 IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Unary_Operator_Static_Invalid()
        {
            Expression<Func<bool>> expression = () => string.Equals("abcd", null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ExpressionHelper.UnaryOperator(methodExpression, sql.IsNull));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Unary_Operator_Instance()
        {
            Expression<Func<bool>> expression = () => new Custom("abcd").IsNull();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.UnaryOperator(methodExpression, sql.IsNull));

            Assert.Equal("@p0 IS NULL", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("abcd")
            }, result.Parameters);
        }

        [Fact]
        public void Unary_Operator_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Equals(null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ExpressionHelper.UnaryOperator(methodExpression, sql.IsNull));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Binary_Operator_Static()
        {
            Expression<Func<bool>> expression = () => string.Equals("abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.BinaryOperator(methodExpression, sql.Eq));

            Assert.Equal("@p0 = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh"
            }, result.Parameters);
        }

        [Fact]
        public void Binary_Operator_Static_Invalid()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh", true);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.BinaryOperator(methodExpression, sql.Eq));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Binary_Operator_Instance()
        {
            Expression<Func<bool>> expression = () => "abcd".Equals("efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.BinaryOperator(methodExpression, sql.Eq));

            Assert.Equal("@p0 = @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh"
            }, result.Parameters);
        }

        [Fact]
        public void Binary_Operator_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Equals("efgh", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.BinaryOperator(methodExpression, sql.Eq));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Ternary_Operator_Static()
        {
            Expression<Func<bool>> expression = () => Custom.Between(15, 10, 20);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.TernaryOperator(methodExpression, sql.Between));

            Assert.Equal("@p0 BETWEEN @p1 AND @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 15,
                ["@p1"] = 10,
                ["@p2"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Ternary_Operator_Static_Invalid()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ExpressionHelper.TernaryOperator(methodExpression, sql.Between));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Ternary_Operator_Instance()
        {
            Expression<Func<bool>> expression = () => new Custom("abcd").Between(10, 20);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.TernaryOperator(methodExpression, sql.Between));

            Assert.Equal("@p0 BETWEEN @p1 AND @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("abcd"),
                ["@p1"] = 10,
                ["@p2"] = 20
            }, result.Parameters);
        }

        [Fact]
        public void Ternary_Operator_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Equals("efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() =>
                ExpressionHelper.TernaryOperator(methodExpression, sql.Between));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Not_Operator_Static()
        {
            Person person = null;
            Expression<Func<bool>> expression = () => Custom.Not(person.Name == "abcd");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Not(methodExpression));

            Assert.Equal("NOT (\"person\".\"Name\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd"
            }, result.Parameters);
        }

        [Fact]
        public void Not_Operator_Static_Bool_Property()
        {
            Person person = null;
            Expression<Func<bool>> expression = () => Custom.Not(person.Active);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Not(methodExpression));

            Assert.Equal("NOT (\"person\".\"Active\" = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Not_Operator_Static_SubQuery()
        {
            IRawQuery query = sql.RawQuery("Subquery");
            Expression<Func<bool>> expression = () => Custom.Not(query);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Not(methodExpression));

            Assert.Equal("NOT ((Subquery) = @p0)", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = true
            }, result.Parameters);
        }

        [Fact]
        public void Not_Operator_Static_Invalid()
        {
            Expression<Func<bool>> expression = () => string.Equals("abcd", null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Not(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Not_Operator_Instance()
        {
            Expression<Func<bool>> expression = () => new Custom("abcd").Not();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.Not(methodExpression));

            Assert.Equal("NOT @p0", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = new Custom("abcd")
            }, result.Parameters);
        }

        [Fact]
        public void Not_Operator_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Equals(null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Not(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Any_Static()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeAny(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "%efgh%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Any_Static_Invalid()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeAny(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Any_Instance()
        {
            Expression<Func<bool>> expression = () => "abcd".Contains("efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeAny(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "%efgh%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Any_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Contains("efgh", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeAny(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Start_Static()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeStart(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Start_Static_Invalid()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeStart(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_Start_Instance()
        {
            Expression<Func<bool>> expression = () => "abcd".Contains("efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeStart(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "efgh%"
            }, result.Parameters);
        }

        [Fact]
        public void Like_Start_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Contains("efgh", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeStart(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_End_Static()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeEnd(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "%efgh"
            }, result.Parameters);
        }

        [Fact]
        public void Like_End_Static_Invalid()
        {
            Expression<Func<int>> expression = () => string.Compare("abcd", "efgh", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeEnd(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Like_End_Instance()
        {
            Expression<Func<bool>> expression = () => "abcd".Contains("efgh");
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.LikeEnd(methodExpression));

            Assert.Equal("@p0 LIKE @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = "abcd",
                ["@p1"] = "%efgh"
            }, result.Parameters);
        }

        [Fact]
        public void Like_End_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Contains("efgh", StringComparison.InvariantCulture);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.LikeEnd(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void ColName_Static()
        {
            Person person = null;
            Expression<Func<object>> expression = () => Custom.ColName(person.Name);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.ColName(methodExpression));

            Assert.Equal("\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void ColName_Static_Invalid()
        {
            Person person = null;
            Expression<Func<bool>> expression = () => string.Equals(person.Name, null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.ColName(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void ColName_Instance()
        {
            Person person = null;
            Expression<Func<object>> expression = () => person.Name.ToString();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile(ExpressionHelper.ColName(methodExpression));

            Assert.Equal("\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void ColName_Instance_Invalid()
        {
            Person person = null;
            Expression<Func<bool>> expression = () => person.Name.Equals(null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.ColName(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void As_Static()
        {
            Person person = null;
            Expression<Func<int>> expression = () => Custom.As<int>(person.Name);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            QueryResult result = engine.Compile((IColumn)ExpressionHelper.As(methodExpression));

            Assert.Equal("\"person\".\"Name\"", result.Sql);
            Assert.Equal(new Dictionary<string, object>(), result.Parameters);
        }

        [Fact]
        public void As_Static_Invalid()
        {
            Expression<Func<bool>> expression = () => string.Equals("abcd", null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.As(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void As_Instance()
        {
            Expression<Func<object>> expression = () => new Custom("abcd").As<int>();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Assert.Equal(new Custom("abcd"), ExpressionHelper.As(methodExpression));
        }

        [Fact]
        public void As_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Equals(null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.As(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Val_Static()
        {
            Person person = new Person() { Name = "abcd" };
            Expression<Func<object>> expression = () => Custom.Val(person.Name);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Assert.Equal("abcd", ExpressionHelper.Val(methodExpression));
        }

        [Fact]
        public void Val_Static_Invalid()
        {
            Expression<Func<bool>> expression = () => string.Equals("abcd", null);
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Exception ex = Assert.Throws<ArgumentException>(() => ExpressionHelper.Val(methodExpression));
            Assert.Equal("Invalid expression, wrong number of parameters.", ex.Message);
        }

        [Fact]
        public void Val_Instance()
        {
            Expression<Func<object>> expression = () => new Custom("abcd").Val();
            MethodCallExpression methodExpression = (MethodCallExpression)expression.Body;

            Assert.Equal(new Custom("abcd"), ExpressionHelper.Val(methodExpression));
        }

        [Fact]
        public void Val_Instance_Invalid()
        {
            Expression<Func<bool>> expression = () => "abcd".Equals(null);
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

            public bool Between(int min, int max) => false;

            public static bool Between(int value, int min, int max) => false;

            public bool Not() => false;

            public static bool Not(bool value) => false;

            public static bool Not(IQueryFragment value) => false;

            public static T ColName<T>(T value) => value;

            public Custom As<T>() => null;

            public static T As<T>(object value) => (T)value;

            public Custom Val() => null;

            public static T Val<T>(T value) => value;
        }
    }
}