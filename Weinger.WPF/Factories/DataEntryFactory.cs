using System;
using System.Linq;
using System.Threading.Tasks;
using Weniger.UiServices;
using Weniger.UiServices.Augmentors;

namespace Weniger.WPF.Factories
{
    internal class DataEntryFactory : IGenerationStrategyFactory
    {
        const int PROPERTY_GRID_THRESHHOLD = 10; 

        private UserItem[] _userItems;

        IXamlFactory dataFormFactory = new DataEntryFormFactory();
        IXamlFactory propertyGridFactory = new PropertyGridFactory();

        public async Task<string> GetXamlAsync(Augmentor augmentor)
        {
            _userItems = await augmentor.OnOutput();

            if (!_userItems.All(i => i is IValueItem) || !_userItems.All(i => i is IHeaderItem))
                throw new NotSupportedException($"{nameof(DataEntryFactory)} requires that all user items implement {nameof(IValueItem)} and {nameof(IHeaderItem)}");

            if (_userItems.Length < PROPERTY_GRID_THRESHHOLD)
            {
                return await dataFormFactory.GetXaml(_userItems);
            }
            else
            {
                return await propertyGridFactory.GetXaml(_userItems);
            }
        }

        

    }
}
