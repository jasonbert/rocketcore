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
	public class CallToActionListModel : RenderingModel<IPage, ICallToActionGroup>, ICallToActionListModel
	{
		public CallToActionListModel(IRenderingModel<IPage, ICallToActionGroup> model)
			: base(model.PageItem, model.RenderingItem)
		{

		}

        public IEnumerable<ICallToAction> CallToActions { get; set; }
		public IPhrase Heading { get; set; }
	}

	public class CallToActionListModel<TRenderingParametersItem> : RenderingModel<IPage, ICallToActionGroup, TRenderingParametersItem>, ICallToActionListModel<TRenderingParametersItem>
		where TRenderingParametersItem : IRenderingParameterWrapper
	{
		public CallToActionListModel(IRenderingModel<IPage, ICallToActionGroup, TRenderingParametersItem> model)
			: base(model.PageItem, model.RenderingItem, model.RenderingParametersItem)
		{

		}

        public IEnumerable<ICallToAction> CallToActions { get; set; }
		public IPhrase Heading { get; set; }
	}

    public interface ICallToActionGroupModel<TModel>
    {
        ICallToActionGroup CallToActionGroup { get; set; }
        IEnumerable<TModel> Links { get; set; }
    }
}
