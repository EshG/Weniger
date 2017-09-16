using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.Xaml.Controls
{
    class DateInput : IControlGenerator
    {
        public string GetXaml(string header, object value, Dictionary<string, string> attachedProperties)
        {
            return $"<DatePicker SelectedDate=\"{header}\" {SharedProperties.ToPropertiesSetters(attachedProperties)} />";
        }
    }
}
