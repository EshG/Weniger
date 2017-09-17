using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using System.Threading;

namespace Weniger.UiServices.ViewModels
{
    public class DataContextManager
    {
        static object staticLock = new object();
        object thisLock = new object();
        
        /// <summary>
        /// Reserve a key befor registering a new context.
        /// The key is reservation-generated as oposed to registration-generated for binding construction porpuses.
        /// </summary>
        /// <param name="augmentor"></param>
        /// <returns></returns>
        public Guid ReserveKey(Augmentors.Augmentor augmentor)
        {
            if (_augmentorsIndex.TryGetValue(augmentor, out Guid key))
            {
                Remove(augmentor);
            }

            augmentor.Disposing += AugmentorDisposing;

            Guid generatedKey = Guid.NewGuid();
            _augmentorsIndex[augmentor] = generatedKey;
            _keysIndex[generatedKey] = augmentor;
            return generatedKey;
        }


        /// <summary>
        /// Registers a new data context.
        /// </summary>
        /// <param name="key">The key must be obtained from <see cref="ReserveKey(Augmentors.Augmentor)"/></param>
        /// <param name="context"></param>
        public void RegisterViewModel(Guid key, object context)
        {
            lock (thisLock)
            {
                if (!_keysIndex.ContainsKey(key))
                    throw new AccessViolationException("Key not registerd, Reserve first");

                if (_contextIndex.ContainsKey(key))
                    throw new ArgumentException("Key already registerd, call reserve first to generate new key");

                _contextIndex[key] = context;
            }
        }

        private void AugmentorDisposing(object sender, EventArgs e)
        {
            Augmentors.Augmentor aug = sender as Augmentors.Augmentor;

            if (_augmentorsIndex.TryGetValue(aug, out Guid key))
            {
                Remove(aug);
            }
        }


        //Exposing the context index as a readonly indexer
        /// <summary>
        /// View Models index
        /// </summary>
        public object this[Guid key]
        {
            get
            {
                if(_contextIndex.TryGetValue(key,out object val))
                {
                    return val;
                }

                return null;
            }
        }


        private void Remove(Augmentors.Augmentor augmentor)
        {
            if (_augmentorsIndex.TryGetValue(augmentor, out Guid key))
            {
                _contextIndex.TryRemove(key,out object val);
                _keysIndex.Remove(key);
                _augmentorsIndex.Remove(augmentor);
            }
        }

        private void Remove(Guid guid)
        {
            if(_keysIndex.TryGetValue(guid,out Augmentors.Augmentor augmentor))
            {
                _contextIndex.TryRemove(guid, out object val);
                _keysIndex.Remove(guid);
                _augmentorsIndex.Remove(augmentor);
            }
        }


        private Dictionary<Guid,Augmentors.Augmentor> _keysIndex = new Dictionary<Guid, Augmentors.Augmentor>();
        private Dictionary<Augmentors.Augmentor, Guid> _augmentorsIndex = new Dictionary<Augmentors.Augmentor, Guid>();

        //Since the view isn't aware of it's originating augmentor, it needs some key to acsess the data context.
        private ConcurrentDictionary<Guid, object> _contextIndex = new ConcurrentDictionary<Guid, object>();


        private static DataContextManager _instance;
        public static DataContextManager Instance
        {
            get
            {
                if (_instance == null)
                    lock(staticLock)
                        _instance = new DataContextManager();

                return _instance;

            }
        }
    }
}
