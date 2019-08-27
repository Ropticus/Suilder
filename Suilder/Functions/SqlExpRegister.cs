using System;
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
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.Eq));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NotEq),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.NotEq));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Like),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.Like));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NotLike),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.NotLike));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Lt),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.Lt));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Le),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.Le));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Gt),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.Gt));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Ge),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.Ge));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.In),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.In));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.NotIn),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.NotIn));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Not), ExpressionHelper.Not);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.IsNull),
                x => ExpressionHelper.SingleOperator(x, SqlBuilder.Instance.IsNull));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.IsNotNull),
                x => ExpressionHelper.SingleOperator(x, SqlBuilder.Instance.IsNotNull));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.All),
                x => ExpressionHelper.SingleOperator(x, SqlBuilder.Instance.All));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Any),
                x => ExpressionHelper.SingleOperator(x, SqlBuilder.Instance.Any));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Exists),
                x => ExpressionHelper.SingleOperator(x, SqlBuilder.Instance.Exists));
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Some),
                x => ExpressionHelper.SingleOperator(x, SqlBuilder.Instance.Some));

            // Extensions
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.Like),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.Like));
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.NotLike),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.NotLike));
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.In),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.In));
            ExpressionProcessor.AddFunction(typeof(SqlExtensions), nameof(SqlExtensions.NotIn),
                x => ExpressionHelper.Operator(x, SqlBuilder.Instance.NotIn));
        }

        /// <summary>
        /// Registers functions.
        /// </summary>
        public static void RegisterFunctions()
        {
            // Special methods
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Function), ExpressionHelper.FunctionWithName);
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
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Coalesce), true);
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Concat), true);
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
            ExpressionProcessor.AddFunction(typeof(String), nameof(String.StartsWith), ExpressionHelper.LikeStart);
            ExpressionProcessor.AddFunction(typeof(String), nameof(String.EndsWith), ExpressionHelper.LikeEnd);
            ExpressionProcessor.AddFunction(typeof(String), nameof(String.Contains), ExpressionHelper.LikeAny);
        }
    }
}