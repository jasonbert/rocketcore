using Rocketcore.Model.Templates.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocketcore.Content.CallToAction.Extensions
{
	public static class QueryableExtensions
	{
		/// <summary>
		/// Attempts to replace the call to action with the target custom call to action if set. Otherwise uses the existing call to action.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IQueryable<ICallToAction> Override(this IQueryable<ICallToAction> source)
		{
			if (source != null)
			{
				var callToActionItems = source.ToList();

				for (int i = 0; i < callToActionItems.Count; i++)
				{
					var callToAction = callToActionItems[i];

					if (callToAction is ICallToActionOverride)
					{
						var callToActionOverride = (ICallToActionOverride)callToAction;

						if (!string.IsNullOrWhiteSpace(callToActionOverride.CallToActionCustom.RawValue))
						{
							callToActionItems[i] = callToActionOverride.CallToActionCustom.GetTarget<ICallToAction>() ?? callToActionItems[i];
						}
					}
				}

				source = callToActionItems.AsQueryable();
			}

			return source;
		}
	}
}
