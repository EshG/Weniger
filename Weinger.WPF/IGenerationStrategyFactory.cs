using System.Threading.Tasks;
using Weniger.UiServices;

namespace Weniger.WPF
{
    internal interface IGenerationStrategyFactory
    {
        Task<string> GetXamlAsync(Augmentor augmentor);
    }
}
