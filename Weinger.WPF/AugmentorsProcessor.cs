using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weniger.UiServices;
using Weniger.UiServices.Augmentors;

namespace Weniger.WPF
{
    class AugmentorsProcessor 
    {
        public async Task<string> Process(IList<Augmentor> Augmentors)
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(Augmentor aug in Augmentors)
            {
                IViewFactory viewFactory = await GetSelector(aug).GetViewFactory(aug);

                Task<UserItem[]> res = aug.OnOutput();
                await res;
                if(res.IsFaulted)
                {
                    //TODO Handle user error
                    Debug.WriteLine(res.Exception.ToString());
                }

                ViewData viewData = await viewFactory.GetViewData(await aug.OnOutput());
                
                sb.Append(viewData.Xaml);
            }

            return await Task.FromResult(sb.ToString());
        }

        private IViewFactorySelector GetSelector(Augmentor aug)
        {
            if (aug is DataEntryAugmentor)
            {
                return new Factories.DataEntryFactory();
            }

            return null;
        }
    }
}
