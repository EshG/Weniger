using System.Collections.Generic;
using System.Threading.Tasks;
using Weniger.UiServices;

namespace Weniger.WPF
{
    public interface IXamlFactory
    {
        Task<string> GetXaml(UserItem[] items);
    }
}
