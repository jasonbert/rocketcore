using Rocketcore.Mvc.Pipelines.GetPlaceholderRenderings;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetChromeData;
using Sitecore.Web.UI.PageModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rocketcore.Mvc.Pipelines.GetChromeData
{
	public class GetDynamicPlaceholderChromeData : GetPlaceholderChromeData
	{
		public override void Process(GetChromeDataArgs args)
		{
			Assert.ArgumentNotNull(args, "args");
			Assert.IsNotNull(args.ChromeData, "Chrome Data");
			if ("placeholder".Equals(args.ChromeType, StringComparison.OrdinalIgnoreCase))
			{
				string placeholderKey = args.CustomData["placeHolderKey"] as string;
				var regex = new Regex(GetDynamicKeyAllowedRenderings.DYNAMIC_KEY_REGEX);
				var match = regex.Match(placeholderKey);
				if (match.Success && match.Groups.Count > 0)
				{
					string newPlaceholderKey = match.Groups[1].Value;
					args.CustomData["placeHolderKey"] = newPlaceholderKey;

					base.Process(args);

					args.CustomData["placeHolderKey"] = placeholderKey;
				}
				else
				{
					base.Process(args);
				}
			}
		}
	}
}
