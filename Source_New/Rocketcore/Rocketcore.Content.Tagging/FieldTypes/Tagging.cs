using System.Web.UI.HtmlControls;
using Rocketcore.Content.Tagging.Controls;
using Sitecore;
using Sitecore.Buckets.Util;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Sitecore.Buckets.Extensions;
using IdHelper = Sitecore.Buckets.Util.IdHelper;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Linq;

namespace Rocketcore.Content.Tagging.FieldTypes
{
	public class Tagging : Control, IContentField
	{
		private string _source;
		private string _itemid;
		private string _filter = string.Empty;
		private int _pageNumber = 0;

		public string GetValue()
		{
			return this.Value;
		}

		public void SetValue(string value)
		{
			Value = value;
		}

		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				Assert.ArgumentNotNull(value, "value");
				_source = value;
			}
		}

		public string ItemID
		{
			get
			{
				return _itemid;
			}
			set
			{
				Assert.ArgumentNotNull(value, "value");
				_itemid = value;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			Assert.ArgumentNotNull(e, "e");
			base.OnLoad(e);

			var value = Sitecore.Context.ClientPage.ClientRequest.Form[this.ID + "_Value"];

			if (value != null)
			{
				if (base.GetViewStateString("Value", string.Empty) != value)
				{
					this.SetModified();
				}

				base.SetViewStateString("Value", value);
			}
		}

		protected void SetModified()
		{
			Sitecore.Context.ClientPage.Modified = true;
		}

		protected override void DoRender(System.Web.UI.HtmlTextWriter output)
		{
			var control = GetControl();

			// Check we have necessary objects 
			Assert.ArgumentNotNull(output, "output");
			Assert.IsNotNull(control, "Invalid control selected for tagging field");
			//Sitecore.Web.UI.HtmlControls.Input
			// Set properties of control
			control.ID = ID;
			control.ControlAttributes = GetControlAttributes();
			control.Value = StringUtil.EscapeQuote(Value);
			control.SelectedValues = GetSelectedValues();
			control.Filter = GenerateFilter();

            var head = WebUtil.FindControlOfType(Sitecore.Context.Page.Page, typeof(HtmlHead));
            if (head != null)
            {
                head.Controls.Add(new Control(){});
            }

			// Output the control
			output.Write(control.Render());
		}

		private Dictionary<string, string> GetSelectedValues()
		{
			var processedSelectedValues = new Dictionary<string, string>();
			var selectedValues = new ListString(Value);

			foreach (var selectedValue in selectedValues)
			{
				var item = Sitecore.Context.ContentDatabase.GetItem(selectedValue);

				processedSelectedValues.Add(selectedValue, item == null ? selectedValue + " " + Translate.Text("[Item not found]") : item.DisplayName);
			}

			return processedSelectedValues;
		}

		private string GenerateFilter()
		{
			var filter = string.Empty;

			if (!string.IsNullOrEmpty(Source))
			{
				var sourceValues = StringUtil.GetNameValues(Source, '=', '&');
				var sourceFilter = sourceValues["Filter"];

				if (!string.IsNullOrEmpty(sourceFilter))
				{
					var sourceFilterValues = StringUtil.GetNameValues(sourceFilter, ':', '|');

					// Location filter
					var locationFilter = MakeFilterQueryable(sourceValues["StartSearchLocation"]);

					// Template filter
					var templateFilter = MakeTemplateFilterQueryable(sourceValues["TemplateFilter"]);

					// Page size
					var pageSize = string.IsNullOrEmpty(sourceValues["PageSize"]) ? 10 : int.Parse(sourceValues["PageSize"]);

					// Construct filter query string
					filter = string.Concat(new object[] { "&location=", IdHelper.NormalizeGuid(string.IsNullOrEmpty(locationFilter) ? Sitecore.Context.ContentDatabase.GetItem(ItemID).GetParentBucketItemOrRootOrSelf().ID.ToString() : locationFilter, true),
														   "&filterText=", sourceFilterValues["FullTextQuery"],
														   "&language=", sourceFilterValues["Language"],
														   "&pageSize=", pageSize,
														   "&sort=", sourceFilterValues["SortField"]
														 });

					if (sourceValues["TemplateFilter"] != null)
					{
						filter += "&template=" + templateFilter;
					}
				}
			}

			return string.Concat(filter + SearchHelper.GetDatabaseUrlParameter("&"));
		}

		private Dictionary<string, string> GetAvailableValues()
		{
			var availableValues = new Dictionary<string, string>();

			if (!string.IsNullOrEmpty(Source))
			{
				var values = StringUtil.GetNameValues(Source, '=', '&');
				var sourceString = values["Filter"];

				if (sourceString != null)
				{
					List<SearchStringModel> searchStringModel = string.IsNullOrEmpty(sourceString) ? null : SearchStringModel.ParseQueryString(sourceString).ToList<SearchStringModel>();
					var dictionary = new Dictionary<string, string>();
					var values2 = StringUtil.GetNameValues(sourceString, ':', '|');

					foreach (string str2 in values2)
					{
						dictionary.Add(str2, values2[str2]);
					}

					// Location filter
					string locationFilter = values["StartSearchLocation"];

					locationFilter = MakeFilterQueryable(locationFilter);
					searchStringModel.Add(new SearchStringModel("location", IdHelper.NormalizeGuid(locationFilter).ToLowerInvariant(), "must"));

					// Template filter
					string templateFilter = values["TemplateFilter"];

					templateFilter = MakeTemplateFilterQueryable(templateFilter);

					// Page size
					var pageSize = string.IsNullOrEmpty(values["PageSize"]) ? 10 : int.Parse(values["PageSize"]);

					// Filter construction
					_filter = string.Concat(new object[] { "&location=", IdHelper.NormalizeGuid(string.IsNullOrEmpty(locationFilter) ? Sitecore.Context.ContentDatabase.GetItem(ItemID).GetParentBucketItemOrRootOrSelf().ID.ToString() : locationFilter, true),
														   "&filterText=", values["FullTextQuery"],
														   "&language=", values["Language"],
														   "&pageSize=", pageSize,
														   "&sort=", values["SortField"]
														 });

					if (values["TemplateFilter"] != null)
					{
						_filter = _filter + "&template=" + templateFilter;
					}

					// Call search
					if (searchStringModel != null)
					{
						using (IProviderSearchContext context = ContentSearchManager.GetIndex((IIndexable)Sitecore.Context.ContentDatabase.GetItem(locationFilter)).CreateSearchContext(SearchSecurityOptions.EnableSecurityCheck))
						{
							var source = LinqHelper.CreateQuery(context, searchStringModel);
							var num = source.Count<SitecoreUISearchResultItem>();

							_pageNumber = num / pageSize;

							if (_pageNumber <= 0)
							{
								_pageNumber = 1;
							}

							var items = (from sitecoreItem in QueryableExtensions.Page<SitecoreUISearchResultItem>(source, _pageNumber - 1, pageSize).ToList<SitecoreUISearchResultItem>()
										 select sitecoreItem.GetItem() into item
										 where item != null
										 select item).ToArray<Item>();
						}
					}
				}
			}

			return availableValues;
		}

		private TaggingControl GetControl()
		{
			var control = new System.Web.UI.UserControl();
			return control.LoadControl("~/Rocketcore/FieldTypes/Tagging.ascx") as TaggingControl; // TODO-TaggingSystem: Refactor file path into config file
		}

		private string MakeFilterQueryable(string locationFilter)
		{
			if ((locationFilter != null) && locationFilter.StartsWith("query:"))
			{
				locationFilter = locationFilter.Replace("->", "=");
				string query = locationFilter.Substring(6);
				bool flag = query.StartsWith("fast:");
				if (!flag)
				{
					QueryParser.Parse(query);
				}
				Item item = flag ? Sitecore.Context.ContentDatabase.GetItem(ItemID).Database.SelectSingleItem(query) : Sitecore.Context.ContentDatabase.GetItem(ItemID).Axes.SelectSingleItem(query);
				locationFilter = item.ID.ToString();
			}
			return locationFilter;
		}

		private string MakeTemplateFilterQueryable(string templateFilter)
		{
			if ((templateFilter != null) && templateFilter.StartsWith("query:"))
			{
				Item[] itemArray;
				templateFilter = templateFilter.Replace("->", "=");
				string str = templateFilter.Substring(6);
				bool flag = str.StartsWith("fast:");
				if (!flag)
				{
					QueryParser.Parse(str);
				}
				if (flag)
				{
					itemArray = Sitecore.Context.ContentDatabase.GetItem(ItemID).Database.SelectItems(str);
				}
				else
				{
					itemArray = Sitecore.Context.ContentDatabase.GetItem(ItemID).Axes.SelectItems(str);
				}
				templateFilter = string.Empty;
				templateFilter = itemArray.Aggregate<Item, string>(templateFilter, (current1, item) => current1 + item.ID.ToString());
			}
			return templateFilter;
		}
	}
}
