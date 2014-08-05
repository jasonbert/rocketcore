using Rocketcore.Mvc.Pipelines.GetPlaceholderRenderings;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rocketcore.Mvc.Extensions;

namespace Rocketcore.Mvc.Events
{
	public class DynamicPlaceholder
	{
		private const int GUID_LENGTH = 36;

		public void OnItemSaved(object sender, EventArgs args)
		{
			var item = Event.ExtractParameter(args, 0) as Item;

			if (item != null)
			{
				//if the rendering reference points to a dynamic placeholder then ensure that that placeholder exists
				//if not then remove the reference. This takes care of the scenario where a scaffolding
				//component has been removed without first removing the Sub-Layouts that may by bound to it.
				var device = Context.Device;

				if (device != null)
				{
					var renderingReferences = item.Visualization.GetRenderings(device, false);

					foreach (var renderingReference in renderingReferences)
					{
						var key = renderingReference.Placeholder;
						var regex = new Regex(GetDynamicKeyAllowedRenderings.DYNAMIC_KEY_REGEX);
						var match = regex.Match(renderingReference.Placeholder);

						if (match.Success && match.Groups.Count > 0)
						{
							//get the rendering reference unique id that we are contained in
							var parentRenderingId = "{" + key.Substring(key.Length - GUID_LENGTH, GUID_LENGTH).ToUpper() + "}";

							Sitecore.Diagnostics.Log.Info("Dynamic Rendering Found: " + key + " | " + parentRenderingId, renderingReference);

							//if this parent renderingReference is not in the current list of rendering references 
							//then the current rendering reference should be removed as it means that the parent
							//rendering reference has been removed by the user without first removing  the children
							if (renderingReferences.All(r => r.UniqueId.ToUpper() != parentRenderingId))
							{
								Sitecore.Diagnostics.Log.Info("Removing Dynamic Rendering: " + renderingReference.UniqueId, renderingReference);

								//use an extension method to remove the orphaned rendering reference
								//from the item's layout definition
								item.RemoveRenderingReference(renderingReference.UniqueId);
							}
						}
					}
				}
			}
		}
	}
}
