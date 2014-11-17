using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
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
		}

		public override object ComputeFieldValue(IIndexable indexable)
		{
			throw new NotImplementedException();
		}

		protected virtual void Initialise(XmlNode configurationNode)
		{
			if (configurationNode != null)
			{
				var configNode = configurationNode.SelectSingleNode("copyFields");

				if (configNode != null)
				{

				}
			}
		}
	}
}
