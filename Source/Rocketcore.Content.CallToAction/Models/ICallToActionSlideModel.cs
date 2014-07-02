using Rocketcore.Model.Templates.UserDefined;

namespace Rocketcore.Content.CallToAction.Models
{
    public interface ICallToActionSlideModel : ICallToActionModel
    {
        ICallToActionSlide Slide { get; }
    }
}