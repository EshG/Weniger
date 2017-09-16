using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weniger.UiServices;
using Weniger.WPF.Xaml;

namespace Weniger.WPF.Factories
{
    internal class DataEntryFormFactory : IXamlFactory
    {
        const int SPLIT_THRESHOLD = 5;
        
        public Task<string> GetXaml(UserItem[] items)
        {
            Stack<UserItem> rightItems = new Stack<UserItem>(items);

            if (rightItems.Count <= SPLIT_THRESHOLD)
                return Task.FromResult(GetForm(rightItems, null));

            List<UserItem> leftItems = new List<UserItem>();
            leftItems.Capacity = SPLIT_THRESHOLD;

            int counter = 0;
            while (rightItems.Count > 0 && counter > SPLIT_THRESHOLD)
            {
                leftItems.Add(rightItems.Pop());
                counter++;
            }

            return Task.FromResult(GetForm(leftItems, rightItems));
        }


        //Form
        private string GetForm(IReadOnlyCollection<UserItem> left, IReadOnlyCollection<UserItem> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            int rowCount = 1;
            int columns = 0;

            if (right != null)
            {
                columns = 2;
            }

            GridBuilder gb = new GridBuilder(rowCount, columns);
            gb.Children.Add(GetColumn(left,0));

            if (right != null)
            {
                gb.Children.Add(GetColumn(right,1));
            }

            return gb.ToString();
        }


        //Column
        private string GetColumn(IReadOnlyCollection<UserItem> items,int column)
        {
            Xaml.GridBuilder gb = new Xaml.GridBuilder(items.Count,2);
            gb.Properties.Add(SharedProperties.GRID_COLUMN, column.ToString());
            int rowsCounter = 0;

            foreach(UserItem item in items)
            {
                string header = ((IHeaderItem)item).Header;
                object value = ((IValueItem)item).Value;

                gb.Children.Add(Xaml.Controls.TextBlock.GetXaml(header, new Dictionary<string, string>()
                {
                    {Xaml.SharedProperties.GRID_COLUMN,"0" },
                    { Xaml.SharedProperties.GRID_ROW,rowsCounter.ToString() }
                }));

                Xaml.Controls.IControlGenerator control = Xaml.Controls.ControlSelector.ForInput(value);

                gb.Children.Add(control.GetXaml(null, value, new Dictionary<string, string>()
                {
                    {Xaml.SharedProperties.GRID_COLUMN,"1" },
                    { Xaml.SharedProperties.GRID_ROW,rowsCounter.ToString() }
                }));

                rowsCounter++;
            }

            return gb.ToString();
        }



    }
}
