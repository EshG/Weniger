using System;
using System.Collections.Generic;
using System.Text;

namespace Weniger
{
    internal class SubscriberReference<T> : IDisposable
    {
        public EventHandler UnsubscribeCallback;

        public IObserver<T> Observer { get; set; }

        public void Dispose()
        {
            UnsubscribeCallback(this, new EventArgs());
        }
    }
}
