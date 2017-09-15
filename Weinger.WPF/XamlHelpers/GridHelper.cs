using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.WPF.XamlHelpers
{
    public static class GridHelper
    {
        public static string GetRowDefinitions(params System.Windows.GridLength[] settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            string definition = "<Grid.ColumnDefinitions>";

            for (int i = 0; i < settings.Length; i++)
            {
                definition += Environment.NewLine + $"<ColumnDefinition Width=\"{ToString(settings[i])}\" />";
            }

            definition += Environment.NewLine + @"</Grid.ColumnDefinitions>";

            Debug.WriteLine(definition);

            return definition;
        }

        public static string ToString(this System.Windows.GridLength length)
        {
            if (length.IsAbsolute)
                return length.Value.ToString();

            if (length.IsAuto)
                return "Auto";

            if (length.IsStar)
                return length.Value + "*";

            throw new NotSupportedException("GridLength settings not supported");
        }
    }
}
