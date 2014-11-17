using Rocketcore.Content.CallToAction.Models;
using Rocketcore.Mvc.Controllers;
//using Rocketcore.Phrases.Extensions;
using Rocketcore.Model.Templates.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Fortis.Search;
using Rocketcore.Search;
using Fortis.Model;
using Rocketcore.Content.CallToAction.Extensions;
using Rocketcore.Content.Global.Extensions;
using Rocketcore.Content.Global;
using Sitecore.ContentSearch.Utilities;

namespace Rocketcore.Content.CallToAction.Controllers
{
	public class CallToActionController : RocketcoreController
	{
		private readonly ISearchManager _searchManager = DependencyResolver.Current.GetService<ISearchManager>();

	    private readonly IAggregateManager _aggregateManager = DependencyResolver.Current.GetService<IAggregateManager>();

		public ActionResult CallToActionList()
		{
			var model = new CallToActionListModel<ICallToActionListOptions>(ItemFactory.GetRenderingContextItems<IPage, ICallToActionGroup, ICallToActionListOptions>(ItemFactory));

			model.CallToActions = model.RenderingParametersItem != null ?
				GetCallToActionItems(model.PageItem, model.RenderingItem, model.RenderingParametersItem).ToList() :
				Enumerable.Empty<ICallToAction>();

			if (model.RenderingParametersItem != null)
			{
				model.Heading = model.RenderingParametersItem.Heading.GetTarget<IPhrase>();
			}

			return View(model);
		}

		public ActionResult CallToActionCompactList()
		{
			var model = new CallToActionListModel<ICallToActionListOptions>(ItemFactory.GetRenderingContextItems<IPage, ICallToActionGroup, ICallToActionListOptions>(ItemFactory));

			model.CallToActions = model.RenderingParametersItem != null ?
				GetCallToActionItems(model.PageItem, model.RenderingItem, model.RenderingParametersItem).ToList() :
				Enumerable.Empty<ICallToAction>();

			if (model.RenderingParametersItem != null)
			{
				model.Heading = model.RenderingParametersItem.Heading.GetTarget<IPhrase>();
			}

			return View(model);
		}

		public ActionResult CallToActionSlider()
		{
			var model = new CallToActionListModel<ICallToActionListOptions>(ItemFactory.GetRenderingContextItems<IPage, ICallToActionGroup, ICallToActionListOptions>(ItemFactory));

			model.CallToActions = model.RenderingParametersItem != null ?
				GetCallToActionItems(model.PageItem, model.RenderingItem, model.RenderingParametersItem).ToList() :
				Enumerable.Empty<ICallToAction>();

			if (model.RenderingParametersItem != null)
			{
				model.Heading = model.RenderingParametersItem.Heading.GetTarget<IPhrase>();
			}

			return View(model);
		}

		public ActionResult CallToActionHeroSlider()
		{
			var model = new CallToActionListModel<ICallToActionListOptions>(ItemFactory.GetRenderingContextItems<IPage, ICallToActionGroup, ICallToActionListOptions>(ItemFactory));

			model.CallToActions = model.RenderingParametersItem != null ?
				GetCallToActionItems(model.PageItem, model.RenderingItem, model.RenderingParametersItem).ToList() :
				Enumerable.Empty<ICallToAction>();

			if (model.RenderingParametersItem != null)
			{
				model.Heading = model.RenderingParametersItem.Heading.GetTarget<IPhrase>();
			}

			return View(model);
		}

	    public ActionResult CallToAction()
		{
			return View(GetCallToActionModel());
		}

        private IEnumerable<ICallToActionModel> GetCallToActionItems(IPage page, ICallToActionGroup source)
        {
            var items = source.CallToActions.GetItems<ICallToAction>();
            IEnumerable<ICallToActionModel> models = items.Select(x => new CallToActionModel(page, x));
            return models;
        }

        private IQueryable<ICallToAction> GetCallToActionItems(IPage context, ICallToActionGroup source, ICallToActionListOptions options)
		{
			IQueryable<ICallToAction> queryable = null;

			if (options == null || options.SelectionMethod.ItemId.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.SelectionMethod.Manual.Id))
			{
				if (source != null)
				{
					queryable = source.CallToActions.GetItems<ICallToAction>().AsQueryable();
				}
			}
			else
			{
				using (var searchContext = _searchManager.SearchContext)
				{
					queryable = _aggregateManager.GetQueryable<ICallToAction>(searchContext, context, options)
												 .AggregateByTags(cta => cta.AggregatedTags, options)
												 .OrderBy(cta => cta.Title)
												 .Take(options)
												 .ToList()
												 .AsQueryable();
				}
			}

			return queryable.Override();
		}

		private ICallToActionModel GetCallToActionModel()
		{
			var model = new CallToActionModel(ItemFactory.GetRenderingContextItems<IPage, ICallToAction>(ItemFactory));

			model.Target = model.RenderingItem as ICallToActionTarget;
			model.Navigation = model.RenderingItem as INavigation;

			return model;
		}
	}
}