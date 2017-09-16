using System.Threading.Tasks;
using Weniger.UiServices;
using Weniger.UiServices.Augmentors;

namespace Weniger.WPF
{
    internal interface IGenerationStrategyFactory
    {
        Task<string> GetXamlAsync(Augmentor augmentor);
    }
}
