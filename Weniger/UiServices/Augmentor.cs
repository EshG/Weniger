using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weniger.UiServices
{
    public abstract class Augmentor
    {
        public abstract Task<UserItem[]> OnOutput();

        public abstract Task OnInput(IInput input,UserItem[] items);

        public abstract Task OnInputValidation(IInput input, UserItem[] items);

        public string InputError { get; set; } = null;

        internal abstract void PreventExternalDerivation();

        private int _EstimatedCount = 0;
        internal virtual int EstimatedCount
        {
            get { return _EstimatedCount; }
        }
    }
}
