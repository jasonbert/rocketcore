using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetPlaceholderRenderings;
using Sitecore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rocketcore.Mvc.Pipelines.GetPlaceholderRenderings
{
	/// <summary>
	/// Handles changing context to the references dynamic "master" renderings settings for inserting the allowed controls for the placeholder and making it editable
	/// </summary>
	public class GetDynamicKeyAllowedRenderings : GetAllowedRenderings
	{
		//text that ends in a GUID
		public const string DYNAMIC_KEY_REGEX = @"(.+)_[\d\w]{8}\-([\d\w]{4}\-){3}[\d\w]{12}";

		public new void Process(GetPlaceholderRenderingsArgs args)
		{
			Assert.IsNotNull(args, "args");

			string placeholderKey = args.PlaceholderKey;
			Regex regex = new Regex(DYNAMIC_KEY_REGEX);
			Match match = regex.Match(placeholderKey);
			if (match.Success && match.Groups.Count > 0)
			{
				placeholderKey = match.Groups[1].Value;
			}
			else
			{
				return;
			}

			// Same as Sitecore.Pipelines.GetPlaceholderRenderings.GetAllowedRenderings but with fake placeholderKey
			Item placeholderItem = null;
			if (ID.IsNullOrEmpty(args.DeviceId))
			{
				placeholderItem = Client.Page.GetPlaceholderItem(placeholderKey, args.ContentDatabase, args.LayoutDefinition);
			}
			else
			{
				using (new DeviceSwitcher(args.DeviceId, args.ContentDatabase))
				{
					placeholderItem = Client.Page.GetPlaceholderItem(placeholderKey, args.ContentDatabase, args.LayoutDefinition);
				}
			}
			List<Item> collection = null;
			if (placeholderItem != null)
			{
				bool flag;
				args.HasPlaceholderSettings = true;
				collection = this.GetRenderings(placeholderItem, out flag);
				if (flag)
				{
					args.CustomData["allowedControlsSpecified"] = true;
					args.Options.ShowTree = false;
				}
			}
			if (collection != null)
			{
				if (args.PlaceholderRenderings == null)
				{
					args.PlaceholderRenderings = new List<Item>();
				}
				args.PlaceholderRenderings.AddRange(collection);
			}
		}

		#region GetAllowedRenderings

		protected virtual List<Item> GetRenderings(Item placeholderItem, out bool allowedControlsSpecified)
		{
			Assert.ArgumentNotNull(placeholderItem, "placeholderItem");
			allowedControlsSpecified = false;
			ListString str = new ListString(placeholderItem["Allowed Controls"]);
			if (str.Count <= 0)
			{
				return null;
			}
			allowedControlsSpecified = true;
			List<Item> list = new List<Item>();
			foreach (string str2 in str)
			{
				Item item = placeholderItem.Database.GetItem(str2);
				if (item != null)
				{
					list.Add(item);
				}
			}
			return list;
		}

		[Obsolete("Deprecated")]
		protected virtual List<Item> GetRenderings(string placeholderKey, string layoutDefinition, Database contentDatabase)
		{
			bool flag;
			Assert.IsNotNull(placeholderKey, "placeholder");
			Assert.IsNotNull(contentDatabase, "database");
			Assert.IsNotNull(layoutDefinition, "layout");
			Item placeholderItem = Client.Page.GetPlaceholderItem(placeholderKey, contentDatabase, layoutDefinition);
			if (placeholderItem == null)
			{
				return null;
			}
			return this.GetRenderings(placeholderItem, out flag);
		}

		public void BaseProcess(GetPlaceholderRenderingsArgs args)
		{
			Assert.IsNotNull(args, "args");
			Item placeholderItem = null;
			if (ID.IsNullOrEmpty(args.DeviceId))
			{
				placeholderItem = Client.Page.GetPlaceholderItem(args.PlaceholderKey, args.ContentDatabase, args.LayoutDefinition);
			}
			else
			{
				using (new DeviceSwitcher(args.DeviceId, args.ContentDatabase))
				{
					placeholderItem = Client.Page.GetPlaceholderItem(args.PlaceholderKey, args.ContentDatabase, args.LayoutDefinition);
				}
			}
			List<Item> renderings = null;
			if (placeholderItem != null)
			{
				bool flag;
				args.HasPlaceholderSettings = true;
				renderings = this.GetRenderings(placeholderItem, out flag);
				if (flag)
				{
					args.Options.ShowTree = false;
				}
			}
			if (renderings != null)
			{
				if (args.PlaceholderRenderings == null)
				{
					args.PlaceholderRenderings = new List<Item>();
				}
				args.PlaceholderRenderings.AddRange(renderings);
			}
		}

		#endregion
	}
}
