using Fortis.Model;
using Rocketcore.Model.Templates.UserDefined;
using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rocketcore.Content.Global
{
	public interface IAggregateManager
	{
		IQueryable<T> ApplyFilters<T>(IProviderSearchContext searchContext, IItemWrapper context, IAggregateOptions options) where T : IItemWrapper;
	}
}
