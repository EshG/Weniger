using System.Collections.Generic;
using System.Text;

namespace Weniger.WPF.Xaml
{
    public static class SharedProperties
    {
        public const string GRID_ROW = "Grid.Row";
        public const string GRID_COLUMN = "Grid.Column";

        public static StringBuilder ToPropertiesSetters(this Dictionary<string,string> properties)
        {

            if (properties == null)
                return new StringBuilder();

            StringBuilder sb = new StringBuilder();

            foreach(KeyValuePair<string,string> pair in properties)
            {
                sb.Append(pair.Key);
                sb.Append("=\"");
                sb.Append(pair.Value+ "\" ");
            }

            return sb;
        }
    }
}
