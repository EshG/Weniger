using System.Threading.Tasks;
using Weniger.UiServices;
using Weniger.UiServices.Augmentors;

namespace Weniger.WPF
{
    internal interface IViewFactorySelector
    {
        Task<IViewFactory> GetViewFactory(Augmentor augmentor);
    }
}
