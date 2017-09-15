using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger.UiServices
{
    public abstract class DataEntryAugmentor : Augmentor, ISaveAugmentor
    {
        public SaveType SaveType { get; set; } = SaveType.Ok;

        internal override void PreventExternalDerivation(){}
    }
}
