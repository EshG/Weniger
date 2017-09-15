using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Weniger.WPF.XamlHelpers
{
    public static class GridHelper
    {
        public static string GetDefinitions(bool rows, params System.Windows.GridLength[] settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            string single = nameof(ColumnDefinition);            
            string axis = nameof(ColumnDefinition.Width);

            if(rows)
            {
                single = nameof(RowDefinition);
                axis = nameof(RowDefinition.Height);
            }

            string multi = single + "s";

            string definition = $"<Grid.{multi}>";

            for (int i = 0; i < settings.Length; i++)
            {
                definition += Environment.NewLine + $"<{single} {axis}=\"{ToString(settings[i])}\" />";
            }

            definition += Environment.NewLine + $@"</Grid.{multi}>";

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
