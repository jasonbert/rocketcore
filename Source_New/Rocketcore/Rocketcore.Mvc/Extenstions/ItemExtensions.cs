using Sitecore;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Rocketcore.Mvc.Extensions
{
	public static class ItemExtensions
	{
		public static void RemoveRenderingReference(this Item item, string renderingReferenceUid)
		{
			var doc = new XmlDocument();
			doc.LoadXml(item[FieldIDs.LayoutField]);

			//remove the orphaned rendering reference from the layout definition
			var node = doc.SelectSingleNode(string.Format("//r[@uid='{0}']", renderingReferenceUid));

			if (node != null && node.ParentNode != null)
			{
				node.ParentNode.RemoveChild(node);

				//save layout definition back to the item
				using (new EditContext(item))
				{
					item[FieldIDs.LayoutField] = doc.OuterXml;
				}
			}
		}
	}
}
