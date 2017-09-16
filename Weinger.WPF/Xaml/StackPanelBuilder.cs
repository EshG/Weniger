using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Weniger.WPF.Xaml
{
    class StackPanelBuilder : IFrameworkElement,IPanel
    {
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public List<string> Children { get; set; } = new List<string>();

        public Orientation Orientation { get; set; } = Orientation.Vertical;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"<{nameof(StackPanel)}");
            sb.Append($" {nameof(Orientation)}=\"{Orientation.ToString()}\" {Properties.ToPropertiesSetters()}>");
           
            foreach(string child in Children)
            {
#if DEBUG
                sb.Append(Environment.NewLine);
#endif
                sb.Append(child);
            }
            sb.Append($"</{nameof(StackPanel)}>");

            return sb.ToString();
        }
    }
}
