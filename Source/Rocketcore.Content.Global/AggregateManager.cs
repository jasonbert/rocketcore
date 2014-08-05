using Fortis.Model;
using Rocketcore.Search;
using Rocketcore.Model.Templates.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Rocketcore.Content.Global.Extensions;
using System.Collections;
using Sitecore.ContentSearch;

namespace Rocketcore.Content.Global
{
	public class AggregateManager : IAggregateManager
	{
		private IItemFactory _itemFactory;

		public AggregateManager(IItemFactory itemFactory)
		{
			_itemFactory = itemFactory;
		}

		public IQueryable<T> GetQueryable<T>(IProviderSearchContext searchContext, IItemWrapper context, IAggregateOptions options)
			where T : IItemWrapper
		{
		    if(options == null)
		        return _itemFactory.Search<T>(searchContext);

		    IQueryable<T> queryable = null;

		    var aggregateBy = options.AggregateBy.ItemId;

			if (aggregateBy.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.AggregateBy.Children.Id))
			{
				if (context != null)
				{
					queryable = context.Children<T>().AsQueryable();
				}
			}
			else if (aggregateBy.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.AggregateBy.Siblings.Id))
			{
				if (context != null)
				{
					queryable = context.Siblings<T>().AsQueryable();
				}
			}
			else
			{
				queryable = _itemFactory.Search<T>(searchContext);
			}

			return queryable;
		}
	}
}
