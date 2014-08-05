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
		private IItemFactory _factory;

		protected IItemFactory ItemFactory
		{
			get { return _factory ?? (_factory = DependencyResolver.Current.GetService<IItemFactory>()); }
		}
	}
}
