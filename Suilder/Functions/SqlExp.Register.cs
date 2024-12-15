using Suilder.Builder;
using Suilder.Core;
using Suilder.Extensions;

namespace Suilder.Functions
{
    public static partial class SqlExp
    {
        /// <summary>
        /// Initialize the class with the implemented functions.
        /// </summary>
        /// <param name="registerSystemFunctions">If <see langword="true"/>, register also system functions.</param>
        public static void Initialize(bool registerSystemFunctions = true)
        {
            // Operators
            RegisterOperators();

            // Functions
            RegisterFunctions();

            // System functions
            if (registerSystemFunctions)
                RegisterSystemFunctions();
        }

        /// <summary>
        /// Registers operators.
        /// </summary>
        public static void RegisterOperators()
        {
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Eq),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Eq));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(NotEq),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotEq));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Like),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Like));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(NotLike),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotLike));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Lt),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Lt));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Le),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Le));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Gt),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Gt));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Ge),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Ge));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(In),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.In));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(NotIn),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotIn));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Not), ExpressionHelper.Not);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(IsNull),
                x => ExpressionHelper.UnaryOperator(x, SqlBuilder.Instance.IsNull));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(IsNotNull),
                x => ExpressionHelper.UnaryOperator(x, SqlBuilder.Instance.IsNotNull));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Between),
                x => ExpressionHelper.TernaryOperator(x, SqlBuilder.Instance.Between));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(NotBetween),
            x => ExpressionHelper.TernaryOperator(x, SqlBuilder.Instance.NotBetween));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(All),
                x => ExpressionHelper.UnaryOperator(x, value => SqlBuilder.Instance.All((IQueryFragment)value)));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Any),
                x => ExpressionHelper.UnaryOperator(x, value => SqlBuilder.Instance.Any((IQueryFragment)value)));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Exists),
                x => ExpressionHelper.UnaryOperator(x, value => SqlBuilder.Instance.Exists((IQueryFragment)value)));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Some),
                x => ExpressionHelper.UnaryOperator(x, value => SqlBuilder.Instance.Some((IQueryFragment)value)));

            // Extensions
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.Like),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Like));
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.NotLike),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotLike));
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.In),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.In));
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.NotIn),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotIn));
        }

        /// <summary>
        /// Registers functions.
        /// </summary>
        public static void RegisterFunctions()
        {
            // Special methods
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Function), ExpressionHelper.FunctionWithName);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Col), ExpressionHelper.Col);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(ColName), ExpressionHelper.ColName);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(As), ExpressionHelper.As);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Val), ExpressionHelper.Val);

            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Abs));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Avg));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(AvgDistinct), FunctionName.Avg,
            (expression, name) =>
            {
                return ExpressionHelper.Function(expression, name).Before(SqlBuilder.Instance.Raw("DISTINCT"));
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Cast));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Ceiling));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Coalesce));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Concat));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Count), (expression, name) =>
            {
                IFunction func = ExpressionHelper.Function(expression, name);
                if (expression.Arguments.Count == 0)
                    func.Add(SqlBuilder.Instance.Col("*"));
                return func;
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(CountDistinct), FunctionName.Count,
            (expression, name) =>
            {
                return ExpressionHelper.Function(expression, name).Before(SqlBuilder.Instance.Raw("DISTINCT"));
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Floor));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(LastInsertId));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Length));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Lower));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(LTrim));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Max));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Min));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Now));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(NullIf));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Replace));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Round));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(RTrim));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Substring));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Sum));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SumDistinct), FunctionName.Sum,
            (expression, name) =>
            {
                return ExpressionHelper.Function(expression, name).Before(SqlBuilder.Instance.Raw("DISTINCT"));
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Trim));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(Upper));
        }

        /// <summary>
        /// Registers system functions.
        /// </summary>
        public static void RegisterSystemFunctions()
        {
            ExpressionProcessor.AddFunction(typeof(string), nameof(string.StartsWith), ExpressionHelper.LikeStart);
            ExpressionProcessor.AddFunction(typeof(string), nameof(string.EndsWith), ExpressionHelper.LikeEnd);
            ExpressionProcessor.AddFunction(typeof(string), nameof(string.Contains), ExpressionHelper.LikeAny);
        }
    }
}