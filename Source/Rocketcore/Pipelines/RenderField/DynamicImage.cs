using Sitecore.Pipelines.RenderField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Rocketcore.Pipelines.RenderField
{
	public class DynamicImage
	{
		Regex srcRegex = new Regex(@"(?<=\src="")[^""]*", RegexOptions.Compiled);

		public void Process(RenderFieldArgs args)
		{
			if (string.Equals(args.FieldTypeKey, "image") && args.Result.FirstPart.Contains("dynamic"))
			{
				var imgNode = HtmlAgilityPack.HtmlNode.CreateNode(args.Result.FirstPart);

				var srcAttribute = imgNode.Attributes.FirstOrDefault(a => a.Name.Equals("src"));
				imgNode.Attributes.Add("data-src", srcAttribute.Value);
				srcAttribute.Value = string.Empty;

				args.Result.FirstPart = imgNode.OuterHtml;
			}
		}
	}
}
