using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.Xaml
{
    internal static class BindingHelper
    {
        public static string GetXaml(UiServices.IVmField vmField, string contextKey)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{");
            sb.Append($"Binding [{contextKey}].{vmField.VmPropertyName}");
            sb.Append("}");


            return sb.ToString();
        }
    }
}
