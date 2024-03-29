namespace Suilder.Engines
{
    /// <summary>
    /// The engine options.
    /// </summary>
    public class EngineOptions
    {
        /// <summary>
        /// The engine name.
        /// </summary>
        /// <value>The engine name.</value>
        public string Name { get; set; }

        /// <summary>
        /// The start character to delimit identifiers.
        /// </summary>
        /// <value>The start character to delimited a name.</value>
        public char EscapeStart { get; set; }

        /// <summary>
        /// The end character to delimit identifiers.
        /// </summary>
        /// <value>The end character to delimit identifiers.</value>
        public char EscapeEnd { get; set; }

        /// <summary>
        /// If <see langword="true"/>, converts all tables and column names to uppercase.
        /// </summary>
        /// <value>If <see langword="true"/>, converts all tables and column names to uppercase.</value>
        public bool UpperCaseNames { get; set; }

        /// <summary>
        /// If <see langword="true"/>, converts all tables and column names to lowercase.
        /// </summary>
        /// <value>If <see langword="true"/>, converts all tables and column names to lowercase.</value>
        public bool LowerCaseNames { get; set; }

        /// <summary>
        /// If <see langword="true"/>, adds the "as" keyword before the alias of a table.
        /// </summary>
        /// <value>If <see langword="true"/>, adds the "as" keyword before the alias of a table.</value>
        public bool TableAs { get; set; } = true;

        /// <summary>
        /// The name of a dummy table for engines that always need a "from" clause.
        /// <para>Set to null for engines that do not need a dummy table.</para>
        /// </summary>
        /// <value>The name of a dummy table for engines that always need a "from" clause.</value>
        public string FromDummyName { get; set; }

        /// <summary>
        /// If the "with" clause needs the "recursive" keyword.
        /// </summary>
        /// <value>If the "with" clause needs the "recursive" keyword.</value>
        public bool WithRecursive { get; set; }

        /// <summary>
        /// If the engine supports "top".
        /// </summary>
        /// <value>If the engine supports "top".</value>
        public bool TopSupported { get; set; } = true;

        /// <summary>
        /// If <see langword="true"/>, adds the top values as parameters.
        /// </summary>
        /// <value>If <see langword="true"/>, adds the top values as parameters.</value>
        public bool TopAsParameters { get; set; } = true;

        /// <summary>
        /// If the engine supports "distinct on".
        /// </summary>
        /// <value>If the engine supports "distinct on".</value>
        public bool DistinctOnSupported { get; set; } = true;

        /// <summary>
        /// If the engine supports "right join".
        /// </summary>
        /// <value>If the engine supports "right join".</value>
        public bool RightJoinSupported { get; set; } = true;

        /// <summary>
        /// If the engine supports "full join".
        /// </summary>
        /// <value>If the engine supports "full join".</value>
        public bool FullJoinSupported { get; set; } = true;

        /// <summary>
        /// The offset style.
        /// </summary>
        /// <value>The offset style.</value>
        public OffsetStyle OffsetStyle { get; set; } = OffsetStyle.Offset;

        /// <summary>
        /// If <see langword="true"/>, adds the offset values as parameters.
        /// </summary>
        /// <value>If <see langword="true"/>, adds the offset values as parameters.</value>
        public bool OffsetAsParameters { get; set; } = true;

        /// <summary>
        /// If the "insert" statement must use a "select union all" to insert multiple rows.
        /// </summary>
        /// <value>If the "insert" statement must use a "select union all" to insert multiple rows.</value>
        public bool InsertWithUnion { get; set; }

        /// <summary>
        /// If the "update" statement must have a "from" clause.
        /// <para>Some engines need it when the table has an alias or a join.</para>
        /// </summary>
        /// <value>If the "update" statement must have a "from" clause.</value>
        public bool UpdateWithFrom { get; set; }

        /// <summary>
        /// If the column must have the table name in the "set" clause.
        /// <para>Some engines need it when the table has a join.</para>
        /// </summary>
        /// <value>If the column must have the table name in the "set" clause.</value>
        public bool UpdateSetWithTableName { get; set; }

        /// <summary>
        /// If the "delete" statement must have an alias before the "from" clause.
        /// <para>Some engines need it when the table has an alias or a join.</para>
        /// </summary>
        /// <value>If the "delete" statement must have an alias before the "from" clause.</value>
        public bool DeleteWithAlias { get; set; }

        /// <summary>
        /// If <see langword="true"/>, adds parentheses to the set operator queries.
        /// </summary>
        /// <value>If <see langword="true"/>, adds parentheses to the set operator queries.</value>
        public bool SetOperatorWrapQuery { get; set; } = true;

        /// <summary>
        /// If the set operator must use a subquery when the value is another set operator.
        /// </summary>
        /// <value>If the set operator must use a subquery when the value is another set operator.</value>
        public bool SetOperatorWithSubQuery { get; set; }

        /// <summary>
        /// The prefix of the parameters.
        /// </summary>
        /// <value>The prefix of the parameters.</value>
        public string ParameterPrefix { get; set; } = "@p";

        /// <summary>
        /// If <see langword="true"/>, adds the index after the parameter name.
        /// </summary>
        /// <value>If <see langword="true"/>, adds the index after the parameter name.</value>
        public bool ParameterIndex { get; set; } = true;

        /// <summary>
        /// If only allow registered functions.
        /// </summary>
        /// <value>If only allow registered functions.</value>
        public bool FunctionsOnlyRegistered { get; set; }
    }

    /// <summary>
    /// The style of a offset.
    /// </summary>
    public enum OffsetStyle
    {
        /// <summary>
        /// Offset not supported.
        /// </summary>
        NotSupported,

        /// <summary>
        /// Use "offset" and "fetch" keywords.
        /// </summary>
        Offset,

        /// <summary>
        /// Use "limit" and "offset" keywords.
        /// </summary>
        Limit
    }
}