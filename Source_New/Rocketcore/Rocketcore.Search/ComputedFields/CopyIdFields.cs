﻿using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Rocketcore.Search.ComputedFields
{
	public class CopyIdFields : CopyFields
	{
		public CopyIdFields(XmlNode configurationNode)
			: base(configurationNode)
		{

		}

		public override object ComputeFieldValue(IIndexable indexable)
		{
			var computedField = new List<string>();

			if (CopyFromFields.Any())
			{
				var item = (Item)(indexable as SitecoreIndexableItem);

				if (item != null)
				{
					foreach (var copyFromField in CopyFromFields)
					{
						var field = item.Fields[copyFromField];
						MultilistField multilistField = field;

						if (multilistField != null)
						{
							foreach (var targetId in multilistField.TargetIDs)
							{
								computedField.Add(IdHelper.NormalizeGuid(targetId));
							}
						}
						else
						{
							LinkField linkField = field;

							if (linkField != null)
							{
								computedField.Add(IdHelper.NormalizeGuid(linkField.TargetID));
							}
						}
					}
				}
			}

			return computedField;
		}
	}
}
