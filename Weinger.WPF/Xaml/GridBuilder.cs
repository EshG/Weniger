using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Weniger.WPF.Xaml
{
    public  class GridBuilder : IFrameworkElement, IPanel
    {
        public List<System.Windows.GridLength> Rows { get; set; } = new List<System.Windows.GridLength>();
        public List<System.Windows.GridLength> Columns { get; set; } = new List<System.Windows.GridLength>();

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public List<string> Children { get; set; } = new List<string>();


        public GridBuilder(){}

        public GridBuilder(int rows,int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                Rows.Add( System.Windows.GridLength.Auto);
            }

            for (int i = 0; i < columns; i++)
            {
                Columns.Add(System.Windows.GridLength.Auto);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"<{nameof(Grid)}");
            sb.Append($" {Properties.ToPropertiesSetters()}>");
            sb.Append(GetDefinitions(true, Rows.ToArray()));
#if DEBUG
            sb.Append(Environment.NewLine);
#endif
            sb.Append(GetDefinitions(false, Columns.ToArray()));

            foreach (string child in Children)
            {
#if DEBUG
                sb.Append(Environment.NewLine);
#endif
                sb.Append(child);
            }
            sb.Append($"</{nameof(Grid)}>");

            return sb.ToString();
        }


        public static string GetDefinitions(bool rows, int count)
        {
            System.Windows.GridLength[] lengths = new System.Windows.GridLength[count];

            for (int i = 0; i < count; i++)
            {
                lengths[i] = System.Windows.GridLength.Auto;
            }

            return GetDefinitions(rows, lengths);
        }

        public static string GetDefinitions(bool rows, params System.Windows.GridLength[] settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            string single = nameof(ColumnDefinition);
            string axis = nameof(ColumnDefinition.Width);

            if (rows)
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
            
            return definition;
        }


        public static string ToString(System.Windows.GridLength length)
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
