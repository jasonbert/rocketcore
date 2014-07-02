using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortis.Model;
using Rocketcore.Model.Templates.UserDefined;

namespace Rocketcore.Content.CallToAction.Models
{


    public class CallToActionSliderModel : CallToActionModel, ICallToActionSlideModel
    {
        public CallToActionSliderModel(IPage pageItem, ICallToAction callToAction)
            : base(pageItem, callToAction)
        {
            Slide = callToAction as ICallToActionSlide;
        }

        public ICallToActionSlide Slide { get; set; }
    }

    public class CallToActionModel : RenderingModel<IPage, ICallToAction>, ICallToActionModel
    {
        public CallToActionModel(IRenderingModel<IPage, ICallToAction> model)
            : base(model.PageItem, model.RenderingItem)
        {
            Navigation = model.RenderingItem as INavigation;
            Target = model.RenderingItem as ICallToActionTarget;
        }

        public CallToActionModel(IPage page, ICallToAction callToActionItem)
            : base(page, callToActionItem)
        {
            Target = callToActionItem as ICallToActionTarget;
            Navigation = callToActionItem as INavigation;
        }

        public ICallToActionTarget Target { get; set; }
        public INavigation Navigation { get; set; }
    }

    public class CallToActionModel<TRenderingParametersItem> : RenderingModel<IPage, ICallToAction, TRenderingParametersItem>, ICallToActionModel<TRenderingParametersItem>
        where TRenderingParametersItem : IRenderingParameterWrapper
    {
        public CallToActionModel(IRenderingModel<IPage, ICallToAction, TRenderingParametersItem> model)
            : base(model.PageItem, model.RenderingItem, model.RenderingParametersItem)
        {
            Navigation = model.RenderingItem as INavigation;
            Target = model.RenderingItem as ICallToActionTarget;
        }

        public CallToActionModel(IPage page, ICallToAction callToActionItem, TRenderingParametersItem options)
            : base(page, callToActionItem, options)
        {
            Navigation = callToActionItem as INavigation;
            Target = callToActionItem as ICallToActionTarget;
        }

        public virtual ICallToActionTarget Target { get; set; }
        public virtual INavigation Navigation { get; set; }
    }
}
