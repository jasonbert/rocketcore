using Fortis.Model;
using Fortis.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocketcore.Model
{
	public class CustomItemFactory : ItemFactory, ICustomItemFactory
	{
		public CustomItemFactory(IContextProvider itemContextProvider)
			: base(itemContextProvider)
		{

		}
	}
}
