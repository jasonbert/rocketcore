using Fortis.Model;
using Fortis.Model.Fields;
using Fortis.Providers;
using Sitecore.ContentSearch;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Rocketcore.Model
{
	public class CustomItemWrapper : ItemWrapper, ICustomItemWrapper
	{
		public CustomItemWrapper(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider)
		{

		}

		public CustomItemWrapper(Guid id, ISpawnProvider spawnProvider)
			: base(id, spawnProvider) { }

		public CustomItemWrapper(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: base(id, lazyFields, spawnProvider) { }

		[IndexField("_tags")]
		public IEnumerable<Guid> AggregatedTags { get; set; }
		[IndexField("_categories")]
		public IEnumerable<Guid> AggregatedCategories { get; set; }

        private string _longID;

        /// <summary>
        /// Gets/Sets the LongID of the item. This is all the Guids of the current items parents in a 
        /// single string. It is used for limiting searchs to the descendants of a particular item.
        /// </summary>
        [IndexField("_path")]
        public string LongID
        {
            get { return IsLazy && !string.IsNullOrWhiteSpace(_longID) ? _longID : ((Item)Original).Paths.LongID; }
            set { _longID = value; }
        }
	}
}
