using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Weniger.UiServices.Augmentors
{
    public abstract class ObservableAugmentor : Augmentor, IObservable<UserItem>
    {
        List<SubscriberReference<UserItem>> _subscribers = new List<SubscriberReference<UserItem>>();
        object thislock = new object();

        private List<SubscriberReference<UserItem>> Subscribers
        {
            get
            {
                lock (thislock)
                {
                    return _subscribers;
                }
            }
        }

        public IDisposable Subscribe(IObserver<UserItem> observer)
        {
            SubscriberReference<UserItem> subscriber = new SubscriberReference<UserItem>() { UnsubscribeCallback = UnSubscribe, Observer = observer };

            Subscribers.Add(subscriber);

            return subscriber;
        }

        protected void NotifyChange(UserItem item)
        {
            foreach (SubscriberReference<UserItem> subRef in Subscribers)
            {
                subRef.Observer.OnNext(item);
            }
        }

        private void UnSubscribe(object sender, EventArgs e)
        {
            _subscribers.Remove(sender as SubscriberReference<UserItem>);
        }

        internal override void PreventExternalDerivation(){}
    }
}
