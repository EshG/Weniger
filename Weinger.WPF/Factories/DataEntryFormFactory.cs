using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weniger.UiServices;

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

        //Row
        private string GetRow(UserItem item)
        {
            return null;
        }


        //Column
        private string GetColumn(IEnumerable<UserItem> items)
        {
            return null;
        }

        //Form
        private string GetForm(IEnumerable<UserItem> left, IEnumerable<UserItem> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));

            return null;
        }
    }
}
