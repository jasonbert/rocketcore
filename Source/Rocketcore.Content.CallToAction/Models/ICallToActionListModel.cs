using Fortis.Model;
using Fortis.Model.Fields;
using Rocketcore.Model.Templates.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocketcore.Content.CallToAction.Models
{
	public interface ICallToActionListModel : IRenderingModel<IPage, ICallToActionGroup>
	{
        IEnumerable<ICallToActionModel<ICallToActionOptions>> CallToActionItems  { get; }
	}

	public interface ICallToActionListModel<TRenderingParametersItem> : IRenderingModel<IPage, ICallToActionGroup, TRenderingParametersItem>
		where TRenderingParametersItem : IRenderingParameterWrapper
	{
        IEnumerable<ICallToActionModel<ICallToActionOptions>> CallToActionItems { get; }
	}

	public interface ICallToActionMediaListModel
	{
        ISizeOptions SizeOptions { get; }
        IEnumerable<ICallToActionMediaModel> CallToActionItems { get; }
	}
}
