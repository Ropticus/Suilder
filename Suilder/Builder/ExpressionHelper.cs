using System;
using System.Linq.Expressions;
using Suilder.Core;

namespace Suilder.Builder
{
    /// <summary>
    /// Helper with predefined delegates to register in <see cref="ExpressionProcessor"/>.
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Delegate for functions.
        /// </summary>
        /// <returns>The delegate.</returns>
        public readonly static Func<MethodCallExpression, string, IFunction> Function = (expression, name) =>
        {
            IFunction func = SqlBuilder.Instance.Function(name);
            if (expression.Object != null)
                func.Add(SqlBuilder.Instance.Val(expression.Object));

            foreach (var arg in expression.Arguments)
            {
                func.Add(SqlBuilder.Instance.Val(arg));
            }
            return func;
        };

        /// <summary>
        /// Delegate for functions with params arg.
        /// </summary>
        /// <returns>The delegate.</returns>
        public readonly static Func<MethodCallExpression, string, IQueryFragment> FunctionParams = (expression, name) =>
        {
            IFunction func = SqlBuilder.Instance.Function(name);
            if (expression.Object != null)
                func.Add(SqlBuilder.Instance.Val(expression.Object));

            for (int i = 0; i < expression.Arguments.Count - 1; i++)
            {
                func.Add(SqlBuilder.Instance.Val(expression.Arguments[i]));
            }
            if (expression.Arguments.Count > 0)
            {
                //Last parameter is params argument
                NewArrayExpression expParams = expression.Arguments[expression.Arguments.Count - 1] as NewArrayExpression;
                if (expParams != null)
                {
                    foreach (var arg in expParams.Expressions)
                    {
                        func.Add(SqlBuilder.Instance.Val(arg));
                    }
                }
            }

            return func;
        };

        /// <summary>
        /// Delegate for operators with two params.
        /// </summary>
        /// <returns>The delegate.</returns>
        public readonly static Func<MethodCallExpression, Func<object, object, IOperator>, IQueryFragment> Operator
            = (expression, op) =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 2)
                    throw new ArgumentException("Invalid expression, wrong number of parameters");

                return op(SqlBuilder.Instance.Val(expression.Arguments[0]),
                    SqlBuilder.Instance.Val(expression.Arguments[1]));
            }
            else if (expression.Arguments.Count == 1)
            {
                return op(SqlBuilder.Instance.Val(expression.Object),
                    SqlBuilder.Instance.Val(expression.Arguments[0]));
            }
            else
            {
                throw new ArgumentException("Invalid expression, wrong number of parameters");
            }
        };

        /// <summary>
        /// Delegate for operators with one param.
        /// </summary>
        /// <returns>The delegate.</returns>
        public readonly static Func<MethodCallExpression, Func<object, IOperator>, IQueryFragment> SingleOperator
            = (expression, op) =>
        {
            if (expression.Object == null)
            {
                if (expression.Arguments.Count != 1)
                    throw new ArgumentException("Invalid expression, wrong number of parameters");

                return op(SqlBuilder.Instance.Val(expression.Arguments[0]));
            }
            else if (expression.Arguments.Count == 0)
            {
                return op(SqlBuilder.Instance.Val(expression.Object));
            }
            else
            {
                throw new ArgumentException("Invalid expression, wrong number of parameters");
            }
        };
    }
}