using System;
using System.Collections.Generic;
using System.Linq;

namespace Suilder.Reflection.Builder.Processors
{
    /// <summary>
    /// The default metadata processor.
    /// <para>This processor can be used for simple metadata, where a collection of keys and values is sufficient,
    /// and you do not need to transform the metadata into another data structure.</para>
    /// <para>The "InheritTable" and "InheritColumns" values are used to determine if the metadata must be inherited.</para>
    /// </summary>
    public class DefaultMetadataProcessor : BaseConfigProcessor, IMetadataProcessor
    {
        /// <summary>
        /// If all table metadata must be inherited.
        /// </summary>
        /// <value>If all table metadata must be inherited.</value>
        protected bool IsInheritAllTable { get; set; }

        /// <summary>
        /// If all member metadata must be inherited.
        /// </summary>
        /// <value>If all member metadata must be inherited.</value>
        protected bool IsInheritAllMembers { get; set; }

        /// <summary>
        /// The keys that must be inherit always.
        /// </summary>
        /// <returns>The keys that must be inherit always.</returns>
        protected ISet<string> InheritAlwaysKeys { get; set; } = new HashSet<string>();

        /// <summary>
        /// The keys that must be ignored.
        /// </summary>
        /// <returns>The keys that must be ignored.</returns>
        protected ISet<string> IgnoreKeys { get; set; } = new HashSet<string>();

        /// <summary>
        /// A list of delegates that determine if a key must be ignored.
        /// </summary>
        /// <returns>A list of delegates that determine if a key must be ignored.</returns>
        protected IList<Func<string, bool>> IgnoreDelegates { get; set; } = new List<Func<string, bool>>();

        /// <summary>
        /// If all metadata must be inherited from the parent class, even if "InheritTable" and "InheritColumns" are
        /// <see langword="false"/>.
        /// </summary>
        /// <param name="inheritAll">If all metadata must be inherited from the parent class.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor InheritAll(bool inheritAll)
        {
            IsInheritAllTable = inheritAll;
            IsInheritAllMembers = inheritAll;
            return this;
        }

        /// <summary>
        /// If all table metadata must be inherited from the parent class, even if "InheritTable" is <see langword="false"/>.
        /// </summary>
        /// <param name="inheritAll">If all table metadata must be inherited from the parent class.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor InheritAllTable(bool inheritAll)
        {
            IsInheritAllTable = inheritAll;
            return this;
        }

        /// <summary>
        /// If all member metadata must be inherited from the parent class, even if "InheritColumns" is
        /// <see langword="false"/>.
        /// </summary>
        /// <param name="inheritAll">If all member metadata must be inherited from the parent class.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor InheritAllMembers(bool inheritAll)
        {
            IsInheritAllMembers = inheritAll;
            return this;
        }

        /// <summary>
        /// Adds the keys that must be inherit always, even if "InheritTable" and "InheritColumns" are
        /// <see langword="false"/>.
        /// </summary>
        /// <param name="inheritKeys">The keys that must be inherit always.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor InheritAlways(params string[] inheritKeys)
        {
            InheritAlwaysKeys.UnionWith(inheritKeys);
            return this;
        }

        /// <summary>
        /// Adds the keys that must be inherit always, even if "InheritTable" and "InheritColumns" are
        /// <see langword="false"/>.
        /// </summary>
        /// <param name="inheritKeys">The keys that must be inherit always.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor InheritAlways(IEnumerable<string> inheritKeys)
        {
            InheritAlwaysKeys.UnionWith(inheritKeys);
            return this;
        }

        /// <summary>
        /// Adds the keys that must be ignored and not processed.
        /// </summary>
        /// <param name="ignoreKeys">The keys that must be ignored and not processed.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor Ignore(params string[] ignoreKeys)
        {
            IgnoreKeys.UnionWith(ignoreKeys);
            return this;
        }

        /// <summary>
        /// Adds the keys that must be ignored and not processed.
        /// </summary>
        /// <param name="ignoreKeys">The keys that must be ignored and not processed.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor Ignore(IEnumerable<string> ignoreKeys)
        {
            IgnoreKeys.UnionWith(ignoreKeys);
            return this;
        }

        /// <summary>
        /// Adds a delegate to determine if a key must be ignored and not processed.
        /// </summary>
        /// <param name="ignoreExpression">The expression.</param>
        /// <returns>The metadata processor.</returns>
        public DefaultMetadataProcessor Ignore(Func<string, bool> ignoreExpression)
        {
            IgnoreDelegates.Add(ignoreExpression);
            return this;
        }

        /// <summary>
        /// Process the configuration.
        /// </summary>
        protected override void ProcessData()
        {
            var levels = GroupByInheranceLevel(ConfigData.ConfigTypes.Values);

            foreach (var level in levels)
            {
                foreach (TableConfig tableConfig in level)
                {
                    TableInfo tableInfo = ResultData.GetConfig(tableConfig.Type);
                    TableInfo parentInfo = ResultData.GetParentConfig(tableConfig.Type);

                    LoadTableMetadata(tableConfig, tableInfo, parentInfo);
                    LoadMemberMetadata(tableConfig, tableInfo, parentInfo);
                }
            }
        }

        /// <summary>
        /// Loads the table metadata.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadTableMetadata(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            foreach (var item in tableConfig.TableMetadata.Where(x => !IsIgnore(x.Key)))
            {
                if (!tableInfo.TableMetadata.ContainsKey(item.Key))
                    tableInfo.TableMetadata.Add(item.Key, item.Value);
            }

            if (parentInfo != null)
            {
                foreach (var item in parentInfo.TableMetadata.Where(x => !IsIgnore(x.Key)))
                {
                    if (IsInheritTable(tableConfig, item.Key))
                    {
                        if (!tableInfo.TableMetadata.ContainsKey(item.Key))
                            tableInfo.TableMetadata.Add(item.Key, item.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Loads the member metadata.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="tableInfo">The table information.</param>
        /// <param name="parentInfo">The parent class table information.</param>
        protected virtual void LoadMemberMetadata(TableConfig tableConfig, TableInfo tableInfo, TableInfo parentInfo)
        {
            foreach (var memberItem in tableConfig.MemberMetadata)
            {
                bool addMetadata = !tableInfo.MemberMetadata.TryGetValue(memberItem.Key, out var memberMetadata);
                if (addMetadata)
                    memberMetadata = new Dictionary<string, object>();

                foreach (var item in memberItem.Value.Where(x => !IsIgnore(x.Key)))
                {
                    if (!memberMetadata.ContainsKey(item.Key))
                        memberMetadata.Add(item.Key, item.Value);
                }

                if (addMetadata && memberMetadata.Any())
                {
                    tableInfo.MemberMetadata.Add(memberItem.Key, memberMetadata);
                }
            }

            if (parentInfo != null)
            {
                foreach (var memberItem in parentInfo.MemberMetadata)
                {
                    IDictionary<string, object> memberMetadata = null;
                    foreach (var item in memberItem.Value.Where(x => !IsIgnore(x.Key)))
                    {
                        if (IsInheritMember(tableConfig, item.Key))
                        {
                            if (memberMetadata == null
                                && !tableInfo.MemberMetadata.TryGetValue(memberItem.Key, out memberMetadata))
                            {
                                memberMetadata = new Dictionary<string, object>();
                                tableInfo.MemberMetadata.Add(memberItem.Key, memberMetadata);
                            }

                            if (!memberMetadata.ContainsKey(item.Key))
                                memberMetadata.Add(item.Key, item.Value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a table metadata key must be inherited.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="key">The metadata key.</param>
        /// <returns><see langword="true"/> if the key must be inherited, otherwise, <see langword="false"/>.</returns>
        protected bool IsInheritTable(TableConfig tableConfig, string key)
        {
            return IsInheritAllTable || tableConfig.InheritTable == true || InheritAlwaysKeys.Contains(key);
        }

        /// <summary>
        /// Determines if a member metadata key must be inherited.
        /// </summary>
        /// <param name="tableConfig">The table configuration.</param>
        /// <param name="key">The metadata key.</param>
        /// <returns><see langword="true"/> if the key must be inherited, otherwise, <see langword="false"/>.</returns>
        protected bool IsInheritMember(TableConfig tableConfig, string key)
        {
            return IsInheritAllMembers || tableConfig.InheritColumns == true || InheritAlwaysKeys.Contains(key);
        }

        /// <summary>
        /// Determines if a metadata key must be ignored.
        /// </summary>
        /// <param name="key">The metadata key.</param>
        /// <returns><see langword="true"/> if the key must be ignored, otherwise, <see langword="false"/>.</returns>
        protected bool IsIgnore(string key)
        {
            return IgnoreKeys.Contains(key) || IgnoreDelegates.Any(x => x(key));
        }
    }
}