using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weniger.UiServices.Augmentors
{
    public abstract class Augmentor
    {
        public abstract Task<UserItem[]> OnOutput();

        
        public virtual Task OnLoad()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnInput(IInput input, UserItem[] items)
        {
            return Task.CompletedTask;
        }
        
        public virtual Task OnInputValidation(IInput input, UserItem[] items)
        {
            return Task.CompletedTask;
        }

        public string InputError { get; set; } = null;

        internal abstract void PreventExternalDerivation();

        private int _EstimatedCount = 0;
        internal virtual int EstimatedCount
        {
            get { return _EstimatedCount; }
        }

        public virtual IGroup Group { get; set; }

        public Guid Id { internal set; get; }

        protected long LongId
        {
            get
            {
                return BitConverter.ToInt64(Id.ToByteArray(), 0);
            }
        }
    }
}
