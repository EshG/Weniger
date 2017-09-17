using System.Collections.Generic;
using System.Threading.Tasks;
using Weniger.UiServices;

namespace Weniger.WPF
{
    public interface IViewFactory
    {
        Task<ViewData> GetViewData(UserItem[] items);
    }

    public class ViewData
    {
        public string Xaml { get; set; }

        public object DataContext { get; set; }
    }
 
}
