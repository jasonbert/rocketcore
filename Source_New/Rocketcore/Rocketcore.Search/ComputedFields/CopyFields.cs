using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Rocketcore.Search.ComputedFields
{
	public class CopyFields : AbstractComputedIndexField
	{
		public readonly List<string> CopyFromFields;

		public CopyFields()
			: this(null)
		{

		}

		public CopyFields(XmlNode configurationNode)
		{
			CopyFromFields = new List<string>();

			Initialise(configurationNode);
		}

		public override object ComputeFieldValue(IIndexable indexable)
		{
			var item = (Item)(indexable as SitecoreIndexableItem);
			var computedField = new List<string>();

			if (item != null)
			{
				foreach(var copyFromField in CopyFromFields)
				{
					var value = item[copyFromField];

					if (!string.IsNullOrEmpty(value))
					{
						computedField.Add(value);
					}
				}
			}

			return computedField;
		}

		protected virtual void Initialise(XmlNode configurationNode)
		{
			if (configurationNode != null)
			{
				var configNode = configurationNode.SelectSingleNode("copyFields");

				if (configNode != null)
				{
					foreach (XmlNode childConfigNode in configNode.ChildNodes)
					{
						if (!string.IsNullOrEmpty(childConfigNode.InnerText))
						{
							CopyFromFields.Add(childConfigNode.InnerText);
						}
					}
				}
			}
		}
	}
}
