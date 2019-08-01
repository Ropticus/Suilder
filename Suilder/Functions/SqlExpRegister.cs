using System;
using System.Linq.Expressions;
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
        /// <param name="registerSystemFunctions">If true, register also system functions.</param>
        public static void Initialize(bool registerSystemFunctions = true)
        {
            //Operators
            RegisterOperators();

            //Function method
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Function), expression =>
            {
                if (expression.Arguments.Count == 0)
                    throw new ArgumentException("Invalid expression");

                ConstantExpression expName = expression.Arguments[0] as ConstantExpression;
                if (expName == null)
                    throw new ArgumentException("Invalid expression");

                IFunction func = SqlBuilder.Instance.Function(((string)expName.Value).ToUpperInvariant());

                if (expression.Arguments.Count > 1)
                {
                    NewArrayExpression expParams = expression.Arguments[1] as NewArrayExpression;
                    if (expParams != null)
                    {
                        foreach (var arg in expParams.Expressions)
                        {
                            func.Add(SqlBuilder.Instance.Val(arg));
                        }
                    }
                }

                return func;
            });

            //Functions
            RegisterFunctions();

            //System functions
            if (registerSystemFunctions)
                RegisterSystemFunctions();
        }

        /// <summary>
        /// Register operators.
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
            ExpressionProcessor.AddFunction(typeof(SqlExp), nameof(SqlExp.Not), expression =>
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression");

                return SqlBuilder.Instance.Not(SqlBuilder.Instance.Op(expression.Arguments[0]));
            });
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

            //Extensions
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
        /// Register functions.
        /// </summary>
        public static void RegisterFunctions()
        {
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
        }

        /// <summary>
        /// Register system functions.
        /// </summary>
        public static void RegisterSystemFunctions()
        {
            ExpressionProcessor.AddFunction(typeof(String), nameof(String.Contains), (expression) =>
            {
                if (expression.Object == null || expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.ToLikeAny((string)SqlBuilder.Instance.Val(expression.Arguments[0])));
            });
            ExpressionProcessor.AddFunction(typeof(String), nameof(String.StartsWith), (expression) =>
            {
                if (expression.Object == null || expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.ToLikeStart((string)SqlBuilder.Instance.Val(expression.Arguments[0])));
            });
            ExpressionProcessor.AddFunction(typeof(String), nameof(String.EndsWith), (expression) =>
            {
                if (expression.Object == null || expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression");

                return SqlBuilder.Instance.Like(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.ToLikeEnd((string)SqlBuilder.Instance.Val(expression.Arguments[0])));
            });
        }
    }
}