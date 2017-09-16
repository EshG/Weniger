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
                Task<string> res = GetGenerator(aug).GetXamlAsync(aug);
                await res;

                if(res.IsFaulted)
                {
                    //TODO Handle user error
                    Debug.WriteLine(res.Exception.ToString());
                }

                sb.Append(res.Result);
            }

            return await Task.FromResult(sb.ToString());
        }

        private IGenerationStrategyFactory GetGenerator(Augmentor aug)
        {
            if (aug is DataEntryAugmentor)
            {
                return new Factories.DataEntryFactory();
            }

            return null;
        }
    }
}
