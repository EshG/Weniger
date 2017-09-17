using System;
using System.Linq;
using System.Threading.Tasks;
using Weniger.UiServices;
using Weniger.UiServices.Augmentors;

namespace Weniger.WPF.Factories
{
    internal class DataEntryFactory : IViewFactorySelector
    {
        const int PROPERTY_GRID_THRESHHOLD = 10; 

        private UserItem[] _userItems;

        IViewFactory dataFormFactory = new DataEntryFormFactory();
        IViewFactory propertyGridFactory = new PropertyGridFactory();

        public async Task<IViewFactory> GetViewFactory(Augmentor augmentor)
        {
            _userItems = await augmentor.OnOutput();

            if (!_userItems.All(i => i is IValueItem) || !_userItems.All(i => i is IHeaderItem))
                throw new NotSupportedException($"{nameof(DataEntryFactory)} requires that all user items implement {nameof(IValueItem)} and {nameof(IHeaderItem)}");

            if (_userItems.Length < PROPERTY_GRID_THRESHHOLD)
            {
                return await Task.FromResult(dataFormFactory);
                 
            }
            else
            {
                return await Task.FromResult(propertyGridFactory);
            }
        }

        

    }
}
