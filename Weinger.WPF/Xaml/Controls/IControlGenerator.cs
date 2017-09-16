using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.Xaml.Controls
{
    interface IControlGenerator
    {
        string GetXaml(string header, object value, Dictionary<string, string> properties);
    }
}
