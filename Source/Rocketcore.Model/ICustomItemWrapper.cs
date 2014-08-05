using Fortis.Model;
using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocketcore.Model
{
	public interface ICustomItemWrapper : IItemWrapper
	{
		[IndexField("_tags")]
		IEnumerable<Guid> AggregatedTags { get; set; }
		[IndexField("_categories")]
		IEnumerable<Guid> AggregatedCategories { get; set; }

        [IndexField("_path")]
        string LongID { get; set; }
	}
}
