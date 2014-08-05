using Fortis.Model;
using Rocketcore.Model.Templates.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocketcore.Content.CallToAction.Models
{
	public interface ICallToActionModel : IRenderingModel<IPage, ICallToAction>
	{
		ICallToActionTarget Target { get; }
		INavigation Navigation { get; }
	}

    public interface ICallToActionModel<TRenderingParametersItem> :
		ICallToActionModel,
        IRenderingModel<IPage, ICallToAction, TRenderingParametersItem>
        where TRenderingParametersItem : IRenderingParameterWrapper
    {

    }
}
