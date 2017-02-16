using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Web.Framework.Kendoui
{
    /// <summary>
    /// Represents a filter expression of Kendo DataSource.
    /// 我的注释
    /// Field， Operator， Value 一起使用  Logic， Filters为null
    /// Logic， Filters 一起使用， Field， Operator， Value都为null
    /// 1)因为在QueryableExtensions Filter<T>方法中
    /// 如果当前Filter Logic != null, 才会foreachFilters
    /// 2)因为在ToExpression方法中
    /// 如果当前Filter有child Filter， 才会返回Logic， Filters组成的值
    /// 如果当前Filter没有child Filter， 返回Field， Operator组成的值
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Gets or sets the name of the sorted field (property). Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
        /// </summary>
        public string Logic { get; set; }

        /// <summary>
        /// Gets or sets the child filter expression. Set to <c>null</c> if there are no child expressions.
        /// </summary>
        public IEnumerable<Filter> Filters { get; set; }

        /// <summary>
        /// Mapping of Kendo DataSource filtering operators to Dynamic Linq
        /// </summary>
        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "DoesNotContain"}
        };

        /// <summary>
        /// Gets a flattened list of all child filter expresions.
        /// </summary>
        /// <returns></returns>
        public IList<Filter> All()
        {
            var filters = new List<Filter>();

            Collect(filters);

            return filters;
        }

        private void Collect(List<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                foreach (Filter filter in Filters)
                {
                    filters.Add(filter);
                    filter.Collect(filters);
                }
            }
            else
            {
                filters.Add(this);
            }
        }

        /// <summary>
        /// Converts the filter expression to a predicate suitable for Dynamic Linq e.g. "Field1 = @1 and Field2.Contains(@2)"
        /// </summary>
        /// <param name="filters">A list of flattened filters.</param>
        public string ToExprssion(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                return "(" + String.Join(" " + Logic + " ", Filters.Select(filter => filter.ToExprssion(filters)).ToArray()) + ")";
            }

            int index = filters.IndexOf(this);

            string comparison = operators[Operator];

            //original code below (case sensitive) commented
            //if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Contains")
            //{
            //    return String.Format("{0}.{1}(@{2})", Field, comparison, index);
            //}

            //we ignore case
            if (comparison == "Contains")
            {
                return String.Format("{0}.IndexOf(@{1}, System.StringComparison.InvariantCultureIgnoreCase) >= 0", Field, index);
            }
            if (comparison == "DoesNotContain")
            {
                return String.Format("{0}.IndexOf(@{1}, System.StringComparison.InvariantCultureIgnoreCase) < 0", Field, index);
            }
            if (comparison == "=" && Value is String)
            {
                //string only
                comparison = "Equals";
                //numeric values use standard "=" char
            }
            if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Equals")
            {
                return String.Format("{0}.{1}(@{2}, System.StringComparison.InvariantCultureIgnoreCase)", Field, comparison, index);
            }

            return String.Format("{0} {1} @{2}", Field, comparison, index);
        }
    }
}
