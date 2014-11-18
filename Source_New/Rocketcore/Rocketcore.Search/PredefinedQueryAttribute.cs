using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Extensions;
using Sitecore.ContentSearch.Linq.Parsing;
using Sitecore.ContentSearch.Linq.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rocketcore.Search
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	public class PredefinedQueryAttribute : Attribute, IPredefinedQueryAttribute, _Attribute
	{
		protected Sitecore.ContentSearch.PredefinedQueryAttribute InnerAttribute { get; set; }

		public PredefinedQueryAttribute(string fieldName, ComparisonType comparison, object value)
		{
			InnerAttribute = new Sitecore.ContentSearch.PredefinedQueryAttribute(fieldName, comparison, value);
		}

		public PredefinedQueryAttribute(string fieldName, ComparisonType comparison, object value, Type type)
		{
			InnerAttribute = new Sitecore.ContentSearch.PredefinedQueryAttribute(fieldName, comparison, value, type);
		}

		public IQueryable<TItem> ApplyFilter<TItem>(IQueryable<TItem> queryable, IIndexValueFormatter valueFormatter)
		{
			PropertyInfo property = typeof(TItem).GetProperty(InnerAttribute.FieldName, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

			return InnerAttribute.ApplyFilter<TItem>(queryable, valueFormatter);
		}
	}
}
