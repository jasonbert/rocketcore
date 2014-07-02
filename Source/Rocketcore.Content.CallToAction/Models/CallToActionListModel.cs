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

        public IEnumerable<ICallToActionModel<ICallToActionOptions>> CallToActionItems { get; set; }
	}

	public class CallToActionListModel<TRenderingParametersItem> : RenderingModel<IPage, ICallToActionGroup, TRenderingParametersItem>, ICallToActionListModel<TRenderingParametersItem>
		where TRenderingParametersItem : IRenderingParameterWrapper
	{
		public CallToActionListModel(IRenderingModel<IPage, ICallToActionGroup, TRenderingParametersItem> model)
			: base(model.PageItem, model.RenderingItem, model.RenderingParametersItem)
		{

		}

        public IEnumerable<ICallToActionModel<ICallToActionOptions>> CallToActionItems { get; set; }
	}

    public interface ICallToActionGroupModel<TModel>
    {
        ICallToActionGroup CallToActionGroup { get; set; }
        IEnumerable<TModel> Links { get; set; }
    }


    public class CallToActionGroupModel<TModel> : ICallToActionGroupModel<TModel>
    {
        public ICallToActionGroup CallToActionGroup { get; set; }

        public IEnumerable<TModel> Links { get; set; }

        public CallToActionGroupModel(ICallToActionGroup callToActionGroup, IEnumerable<TModel> actionItems)
        {
            CallToActionGroup = callToActionGroup;
            Links = actionItems;
        }

    }


}
