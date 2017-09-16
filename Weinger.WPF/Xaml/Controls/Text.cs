using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.Xaml.Controls
{
    class TextBlock : IControlGenerator
    {
        public string GetXaml(string header, object value, Dictionary<string, string> properties)
        {
            return GetXaml(header, properties);
        }

        public static string GetXaml(string header, Dictionary<string, string> attachedProperties)
        {
            return $"<TextBlock Text=\"{header}\" {SharedProperties.ToPropertiesSetters(attachedProperties)} />";
        }
    }


    class TextBox : IControlGenerator
    {
        public string GetXaml(string header, object value, Dictionary<string, string> properties)
        {
            return GetXaml(value, properties);
        }

        public static string GetXaml(object value, Dictionary<string, string> attachedProperties)
        {
            return $"<TextBox Text=\"{value}\" {SharedProperties.ToPropertiesSetters(attachedProperties)} />";
        }
    }
}
