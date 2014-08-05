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
        IEnumerable<ICallToAction> CallToActions  { get; }
		IPhrase Heading { get; }
	}

	public interface ICallToActionListModel<TRenderingParametersItem> :
		ICallToActionListModel,
		IRenderingModel<IPage, ICallToActionGroup, TRenderingParametersItem>
		where TRenderingParametersItem : IRenderingParameterWrapper
	{

	}
}
