using System.Globalization;
using Suilder.Builder;
using Suilder.Engines;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="ISqlType"/>.
    /// </summary>
    public class SqlType : ISqlType
    {
        /// <summary>
        /// The type name.
        /// </summary>
        /// <value>The type name.</value>
        protected string Name { get; set; }

        /// <summary>
        /// The length of the type or precision for numbers.
        /// </summary>
        /// <value>The length of the type or precision for numbers.</value>
        protected int? Length { get; set; }

        /// <summary>
        /// The scale of the type.
        /// </summary>
        /// <value>The scale of the type.</value>
        protected int? Scale { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlType"/> class.
        /// </summary>
        /// <param name="name">The type name.</param>
        /// <param name="length">The length of the type or precision for numbers.</param>
        /// <param name="scale">The scale of the type.</param>
        public SqlType(string name, int? length, int? scale)
        {
            Name = name;
            Length = length;
            Scale = scale;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public virtual void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            queryBuilder.Write(Name);
            if (Length != null)
            {
                queryBuilder.Write("(").Write(Length.Value.ToString(CultureInfo.InvariantCulture));
                if (Scale != null)
                {
                    queryBuilder.Write(", ").Write(Scale.Value.ToString(CultureInfo.InvariantCulture));
                }
                queryBuilder.Write(")");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Write(Name)
                .IfNotNull(Length, x => b.Write("(").Write(x.ToString())
                    .IfNotNull(Scale, s => b.Write(", ").Write(s.ToString()))
                    .Write(")")));
        }
    }
}