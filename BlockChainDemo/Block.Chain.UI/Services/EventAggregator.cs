using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BlockchainAPI.Services
{
    public class EventAggregator : IEventAggregator
    {
        private static ConcurrentDictionary<Type, object> _events = new ConcurrentDictionary<Type, object>();
        public T GetEvent<T>() => (T)_events.GetOrAdd(typeof(T), _ => Activator.CreateInstance(typeof(T)));
    }

    public class WpfCompositeEvent<TArgs>
    {
        interface IInternalSubscription : ISubscription<TArgs>
        {
            int OrderIndex { get; }
        }
        class InstanceSubscription : IInternalSubscription
        {
            public InstanceSubscription(int orderIndex, Action<TArgs> a)
            {
                this.Action = a;
                this.OrderIndex = orderIndex;
            }
            public void Invoke(TArgs arg) => this.Action(arg);
            public Action<TArgs> Action { get; set; }
            public int OrderIndex { get; set; }
        }

        class SubscriptionToken : ISubscriptionToken
        {
            internal SubscriptionToken(Action remover) => this.Remover = remover;
            public void Unregister() => this.Remover();
            private Action Remover { get; set; }
        }

        public ISubscriptionToken RegisterHandlerInstance(Action<TArgs> handler, int orderIndex = 0)
        {
            var sub = new InstanceSubscription(orderIndex, handler);
            _subscriptions.Add(sub);
            _subscriptions.Sort((y, x) => y.OrderIndex.CompareTo(x.OrderIndex));
            return new SubscriptionToken(() => _subscriptions.Remove(sub));
        }
        public void Publish(TArgs arg) => this._subscriptions.ForEach(_ => _.Invoke(arg));

        List<IInternalSubscription> _subscriptions = new List<IInternalSubscription>();
        public void UnregisterHandlerInstance(ISubscriptionToken handlerInstance)
        {

        }
        public void UnregisterHandlerInstance(Action<TArgs> subcriber)
        {

        }
        public void UnregisterAllHandlerInstances() => this._subscriptions.Clear();
    }

    public interface IEventAggregator
    {
        T GetEvent<T>();
    }

    public interface ISubscriptionToken
    {
        void Unregister();
    }
    public interface ISubscription<in TArg>
    {
        void Invoke(TArg arg);
    }
}
