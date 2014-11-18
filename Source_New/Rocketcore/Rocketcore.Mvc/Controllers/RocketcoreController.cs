using Fortis.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Rocketcore.Mvc.Controllers
{
	public abstract class RocketcoreController : Controller
	{
		private readonly IItemFactory _itemFactory;

		public RocketcoreController(IItemFactory itemFactory)
		{
			_itemFactory = itemFactory;
		}

		protected IItemFactory ItemFactory
		{
			get { return _itemFactory; }
		}
	}
}
