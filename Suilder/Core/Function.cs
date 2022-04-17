using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;
using Suilder.Functions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IFunction"/>.
    /// </summary>
    public class Function : ValList, IFunction
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        /// <value>The name of the function.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// The value to write before the arguments of the function.
        /// </summary>
        /// <value>The value to write before the arguments of the function.</value>
        public IQueryFragment BeforeArgs { get; protected set; }

        /// <summary>
        /// The arguments of the function.
        /// </summary>
        /// <value>The arguments of the function.</value>
        public IReadOnlyList<object> Args => Values;

        /// <summary>
        /// Initializes a new instance of the <see cref="Function"/> class.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        public Function(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Sets a value to write before the arguments of the function.
        /// </summary>
        /// <param name="value">The value to write before the arguments of the function.</param>
        /// <returns>The function.</returns>
        public IFunction Before(IQueryFragment value)
        {
            BeforeArgs = value;
            return this;
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        IFunction IFunction.Add(object value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        IFunction IFunction.Add(params object[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        IFunction IFunction.Add(IEnumerable<object> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="value">The value to add to the end of the <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        IFunction IFunction.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        IFunction IFunction.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IFunction"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IFunction"/>.</param>
        /// <returns>The function.</returns>
        IFunction IFunction.Add(IEnumerable<Expression<Func<object>>> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>

        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            IFunctionInfo funcInfo = engine.GetFunction(Name);

            if (engine.Options.FunctionsOnlyRegistered && funcInfo == null)
                throw new InvalidConfigurationException($"Function \"{Name}\" is not registered.");

            if (funcInfo?.Compile != null)
            {
                funcInfo.Compile(queryBuilder, engine, funcInfo.Name ?? Name, this);
            }
            else
            {
                queryBuilder.Write(funcInfo?.Name ?? Name).Write("(");

                if (BeforeArgs != null)
                    queryBuilder.WriteFragment(BeforeArgs).Write(" ");

                if (Values.Count > 0)
                    base.Compile(queryBuilder, engine);

                queryBuilder.Write(")");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write(Name).Write("(")
                .IfNotNull(BeforeArgs, () => b.WriteFragment(BeforeArgs).Write(" "))
                .Join(", ", Values, x => b.WriteValue(x)).Write(")"));
        }
    }
}