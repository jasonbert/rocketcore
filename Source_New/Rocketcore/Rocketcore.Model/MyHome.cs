using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Converters;
using Sitecore.ContentSearch.Linq.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocketcore.Model
{
	[PredefinedQuery("TemplateId", ComparisonType.Equal, "{44D657D3-6758-4C9C-B4C5-9200B424F186}", typeof(Guid))]
	public class MyHome
	{
		[IndexField("title")]
		public string Title { get; set; }

		[IndexField("_name")]
		public string Name { get; set; }

		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_template")]
		public Guid TemplateId { get; set; }
	}
}
