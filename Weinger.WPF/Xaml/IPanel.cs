using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.Xaml
{
    interface IPanel
    {
        List<string> Children { get; set; }
    }
}
