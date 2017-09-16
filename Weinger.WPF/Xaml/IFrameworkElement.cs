using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.Xaml
{
    interface IFrameworkElement
    {
        Dictionary<string, string> Properties { get; set; }
    }
}
