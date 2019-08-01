using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IOffset"/>.
    /// </summary>
    public class OffsetFetch : IOffset
    {
        /// <summary>
        /// The number of rows to skip.
        /// </summary>
        /// <value>The number of rows to skip.</value>
        protected object OffsetValue { get; set; }

        /// <summary>
        /// The number of rows to return.
        /// </summary>
        /// <value>The number of rows to return.</value>
        protected object FetchValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OffsetFetch"/> class.
        /// </summary>
        public OffsetFetch()
        {
        }

        /// <summary>
        /// Adds an "offset" clause.
        /// </summary>
        /// <param name="offset">The number of rows to skip.</param>
        /// <returns>The "offset fetch" clause.</returns>
        public virtual IOffset Offset(object offset)
        {
            OffsetValue = offset;
            return this;
        }

        /// <summary>
        /// Adds a "fetch" clause.
        /// </summary>
        /// <param name="fetch">The number of rows to return.</param>
        /// <returns>The "offset fetch" clause.</returns>
        public virtual IOffset Fetch(object fetch)
        {
            FetchValue = fetch;
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            switch (engine.Options.OffsetStyle)
            {
                case OffsetStyle.Offset:
                    queryBuilder.Write("OFFSET ");

                    object offsetValue = OffsetValue ?? 0;
                    if (engine.Options.OffsetAsParameters || offsetValue is IQueryFragment)
                        queryBuilder.WriteValue(offsetValue);
                    else
                        queryBuilder.Write(offsetValue.ToString());

                    queryBuilder.Write(" ROWS");

                    if (FetchValue != null)
                    {
                        queryBuilder.Write(" FETCH NEXT ");

                        if (engine.Options.OffsetAsParameters || FetchValue is IQueryFragment)
                            queryBuilder.WriteValue(FetchValue);
                        else
                            queryBuilder.Write(FetchValue.ToString());

                        queryBuilder.Write(" ROWS ONLY");
                    }
                    break;
                case OffsetStyle.Limit:
                    queryBuilder.Write("LIMIT ");

                    object fetchValue = FetchValue ?? long.MaxValue;
                    if (engine.Options.OffsetAsParameters || fetchValue is IQueryFragment)
                        queryBuilder.WriteValue(fetchValue);
                    else
                        queryBuilder.Write(fetchValue.ToString());

                    if (OffsetValue != null)
                    {
                        queryBuilder.Write(" OFFSET ");

                        if (engine.Options.OffsetAsParameters || OffsetValue is IQueryFragment)
                            queryBuilder.WriteValue(OffsetValue);
                        else
                            queryBuilder.Write(OffsetValue.ToString());
                    }
                    break;
                default:
                    throw new ClauseNotSupportedException("Offset clause is not supported in this engine.");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b
                .IfNotNull(OffsetValue, x => b.Write("OFFSET ").WriteValue(x).Write(" "))
                .IfNotNull(FetchValue, x => b.Write("FETCH ").WriteValue(x)).Write(" ")
                .RemoveLast(1));
        }
    }
}