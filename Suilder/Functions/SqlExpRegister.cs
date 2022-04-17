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
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Eq),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Eq));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NotEq),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotEq));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Like),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Like));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NotLike),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotLike));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Lt),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Lt));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Le),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Le));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Gt),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Gt));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Ge),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.Ge));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.In),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.In));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NotIn),
                x => ExpressionHelper.BinaryOperator(x, SqlBuilder.Instance.NotIn));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Not), ExpressionHelper.Not);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.IsNull),
                x => ExpressionHelper.UnaryOperator(x, SqlBuilder.Instance.IsNull));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.IsNotNull),
                x => ExpressionHelper.UnaryOperator(x, SqlBuilder.Instance.IsNotNull));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Between),
                x => ExpressionHelper.TernaryOperator(x, SqlBuilder.Instance.Between));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NotBetween),
            x => ExpressionHelper.TernaryOperator(x, SqlBuilder.Instance.NotBetween));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.All),
                x => ExpressionHelper.UnaryOperator(x, value => SqlBuilder.Instance.All((IQueryFragment)value)));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Any),
                x => ExpressionHelper.UnaryOperator(x, value => SqlBuilder.Instance.Any((IQueryFragment)value)));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Exists),
                x => ExpressionHelper.UnaryOperator(x, value => SqlBuilder.Instance.Exists((IQueryFragment)value)));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Some),
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
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Function), ExpressionHelper.FunctionWithName);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.ColName), ExpressionHelper.ColName);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.As), ExpressionHelper.As);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Val), ExpressionHelper.Val);

            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Abs));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Avg));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.AvgDistinct), FunctionName.Avg,
            (expression, name) =>
            {
                return ExpressionHelper.Function(expression, name).Before(SqlBuilder.Instance.Raw("DISTINCT"));
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Cast));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Ceiling));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Coalesce));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Concat));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Count), (expression, name) =>
            {
                IFunction func = ExpressionHelper.Function(expression, name);
                if (expression.Arguments.Count == 0)
                    func.Add(SqlBuilder.Instance.Col("*"));
                return func;
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.CountDistinct), FunctionName.Count,
            (expression, name) =>
            {
                return ExpressionHelper.Function(expression, name).Before(SqlBuilder.Instance.Raw("DISTINCT"));
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Floor));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.LastInsertId));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Length));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Lower));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.LTrim));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Max));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Min));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Now));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NullIf));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Replace));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Round));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.RTrim));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Substring));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Sum));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.SumDistinct), FunctionName.Sum,
            (expression, name) =>
            {
                return ExpressionHelper.Function(expression, name).Before(SqlBuilder.Instance.Raw("DISTINCT"));
            });
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Trim));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Upper));
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