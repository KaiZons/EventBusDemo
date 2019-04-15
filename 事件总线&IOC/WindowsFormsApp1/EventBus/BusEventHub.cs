using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus
{
    /// <summary>
    /// BusEvent hub responsible for taking subscriptions/publications and delivering of bus events.
    /// </summary>
    public sealed class BusEventHub : IBusEventHub
    {
        #region Private Types and Interfaces
        private class WeakBusEventSubscription<TBusEvent> : IBusEventSubscription
            where TBusEvent : class, IBusEvent
        {
            protected BusEventSubscriptionToken m_subscriptionToken;
            protected WeakReference m_deliveryAction;
            protected WeakReference m_eventFilter;

            public BusEventSubscriptionToken SubscriptionToken
            {
                get { return m_subscriptionToken; }
            }

            public bool ShouldAttemptDelivery(IBusEvent busEvent)
            {
                if (!(busEvent is TBusEvent))
                {
                    return false;
                }

                if (!m_deliveryAction.IsAlive)
                {
                    return false;
                }

                if (!m_eventFilter.IsAlive)
                {
                    return false;
                }  

                return ((Func<TBusEvent, bool>)m_eventFilter.Target).Invoke(busEvent as TBusEvent);
            }

            public void Deliver(IBusEvent busEvent)
            {
                if (!(busEvent is TBusEvent))
                {
                    throw new ArgumentException("event is not the correct type");
                }


                if (!m_deliveryAction.IsAlive)
                {
                    return;
                }
                    

                ((Action<TBusEvent>)m_deliveryAction.Target).Invoke(busEvent as TBusEvent);
            }

            /// <summary>
            /// Initializes a new instance of the WeakBusEventSubscription class.
            /// </summary>
            /// <param name="destination">Destination object</param>
            /// <param name="deliveryAction">Delivery action</param>
            /// <param name="eventFilter">Filter function</param>
            public WeakBusEventSubscription(BusEventSubscriptionToken subscriptionToken, Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter)
            {
                if (subscriptionToken == null)
                {
                    throw new ArgumentNullException("subscriptionToken");
                }

                if (deliveryAction == null)
                {
                    throw new ArgumentNullException("deliveryAction");
                }

                if (eventFilter == null)
                {
                    throw new ArgumentNullException("eventFilter");
                }
                    
                m_subscriptionToken = subscriptionToken;
                m_deliveryAction = new WeakReference(deliveryAction);
                m_eventFilter = new WeakReference(eventFilter);
            }
        }

        private class StrongBusEventSubscription<TBusEvent> : IBusEventSubscription
            where TBusEvent : class, IBusEvent
        {
            protected BusEventSubscriptionToken m_subscriptionToken;
            protected Action<TBusEvent> m_deliveryAction;
            protected Func<TBusEvent, bool> m_eventFilter;

            public BusEventSubscriptionToken SubscriptionToken
            {
                get { return m_subscriptionToken; }
            }

            public bool ShouldAttemptDelivery(IBusEvent busEvent)
            {
                if (!(busEvent is TBusEvent))
                {
                    return false;
                }

                return m_eventFilter.Invoke(busEvent as TBusEvent);
            }

            public void Deliver(IBusEvent busEvent)
            {
                if (!(busEvent is TBusEvent))
                {
                    throw new ArgumentException("event is not the correct type");
                } 

                m_deliveryAction.Invoke(busEvent as TBusEvent);
            }

            /// <summary>
            /// Initializes a new instance of the StrongBusEventSubscription class.
            /// </summary>
            /// <param name="destination">Destination object</param>
            /// <param name="deliveryAction">Delivery action</param>
            /// <param name="eventFilter">Filter function</param>
            public StrongBusEventSubscription(BusEventSubscriptionToken subscriptionToken, Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter)
            {
                if (subscriptionToken == null)
                {
                    throw new ArgumentNullException("subscriptionToken");
                }

                if (deliveryAction == null)
                {
                    throw new ArgumentNullException("deliveryAction");
                }

                if (eventFilter == null)
                {
                    throw new ArgumentNullException("eventFilter");
                }

                m_subscriptionToken = subscriptionToken;
                m_deliveryAction = deliveryAction;
                m_eventFilter = eventFilter;
            }
        }
        #endregion

        #region Subscription dictionary
        private class SubscriptionItem
        {
            public IBusEventProxy Proxy { get; private set; }
            public IBusEventSubscription Subscription { get; private set; }

            public SubscriptionItem(IBusEventProxy proxy, IBusEventSubscription subscription)
            {
                Proxy = proxy;
                Subscription = subscription;
            }
        }

        private readonly object m_subscriptionsPadlock = new object();
        private readonly Dictionary<Type, List<SubscriptionItem>> m_subscriptions = new Dictionary<Type, List<SubscriptionItem>>();
        #endregion

        #region Public API
        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action.
        /// All references are held with strong references
        /// 
        /// All bus events of this type will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, (m) => true, true, DefaultBusEventProxy.Instance);
        }

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action.
        /// Bus events will be delivered via the specified proxy.
        /// All references (apart from the proxy) are held with strong references
        /// 
        /// All bus events of this type will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="proxy">Proxy to use when delivering the events</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, IBusEventProxy proxy) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, (m) => true, true, proxy);
        }

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action.
        /// 
        /// All bus events of this type will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="useStrongReferences">Use strong references to destination and deliveryAction </param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, bool useStrongReferences) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, (m) => true, useStrongReferences, DefaultBusEventProxy.Instance);
        }

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action.
        /// Bus events will be delivered via the specified proxy.
        /// 
        /// All bus events of this type will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="useStrongReferences">Use strong references to destination and deliveryAction </param>
        /// <param name="proxy">Proxy to use when delivering the bus events</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, bool useStrongReferences, IBusEventProxy proxy) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, (m) => true, useStrongReferences, proxy);
        }

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action with the given filter.
        /// All references are held with WeakReferences
        /// 
        /// Only bus events that "pass" the filter will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, eventFilter, true, DefaultBusEventProxy.Instance);
        }

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action with the given filter.
        /// Bus events will be delivered via the specified proxy.
        /// All references (apart from the proxy) are held with WeakReferences
        /// 
        /// Only bus events that "pass" the filter will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="proxy">Proxy to use when delivering the bus events</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter, IBusEventProxy proxy) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, eventFilter, true, proxy);
        }

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action with the given filter.
        /// All references are held with WeakReferences
        /// 
        /// Only bus events that "pass" the filter will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="useStrongReferences">Use strong references to destination and deliveryAction </param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter, bool useStrongReferences) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, eventFilter, useStrongReferences, DefaultBusEventProxy.Instance);
        }

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action with the given filter.
        /// Bus events will be delivered via the specified proxy.
        /// All references are held with WeakReferences
        /// 
        /// Only bus events that "pass" the filter will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="useStrongReferences">Use strong references to destination and deliveryAction </param>
        /// <param name="proxy">Proxy to use when delivering the bus events</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        public BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter, bool useStrongReferences, IBusEventProxy proxy) where TBusEvent : class, IBusEvent
        {
            return AddSubscriptionInternal<TBusEvent>(deliveryAction, eventFilter, useStrongReferences, proxy);
        }

        /// <summary>
        /// Unsubscribe from a particular bus event type.
        /// 
        /// Does not throw an exception if the subscription is not found.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="subscriptionToken">Subscription token received from Subscribe</param>
        public void Unsubscribe<TBusEvent>(BusEventSubscriptionToken subscriptionToken) where TBusEvent : class, IBusEvent
        {
            RemoveSubscriptionInternal<TBusEvent>(subscriptionToken);
        }

        /// <summary>
        /// Publish a bus event to any subscribers
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="event">Bus event to deliver</param>
        public void Publish<TBusEvent>(TBusEvent busEvent) where TBusEvent : class, IBusEvent
        {
            PublishInternal<TBusEvent>(busEvent);
        }

        /// <summary>
        /// Publish a bus event to any subscribers asynchronously
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="event">Bus event to deliver</param>
        public void PublishAsync<TBusEvent>(TBusEvent busEvent) where TBusEvent : class, IBusEvent
        {
            PublishAsyncInternal<TBusEvent>(busEvent, null);
        }

        /// <summary>
        /// Publish a bus event to any subscribers asynchronously
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="event">Bus event to deliver</param>
        /// <param name="callback">AsyncCallback called on completion</param>
        public void PublishAsync<TBusEvent>(TBusEvent busEvent, AsyncCallback callback) where TBusEvent : class, IBusEvent
        {
            PublishAsyncInternal<TBusEvent>(busEvent, callback);
        }
        #endregion

        #region Internal Methods
        private BusEventSubscriptionToken AddSubscriptionInternal<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter, bool strongReference, IBusEventProxy proxy)
                where TBusEvent : class, IBusEvent
        {
            if (deliveryAction == null)
            {
                throw new ArgumentNullException("deliveryAction");
            }

            if (eventFilter == null)
            {
                throw new ArgumentNullException("eventFilter");
            }

            if (proxy == null)
            {
                throw new ArgumentNullException("proxy");
            }    

            lock (m_subscriptionsPadlock)
            {
                List<SubscriptionItem> currentSubscriptions;

                if (!m_subscriptions.TryGetValue(typeof(TBusEvent), out currentSubscriptions))
                {
                    currentSubscriptions = new List<SubscriptionItem>();
                    m_subscriptions[typeof(TBusEvent)] = currentSubscriptions;
                }

                var subscriptionToken = new BusEventSubscriptionToken(this, typeof(TBusEvent));

                IBusEventSubscription subscription;
                if (strongReference)
                {
                    subscription = new StrongBusEventSubscription<TBusEvent>(subscriptionToken, deliveryAction, eventFilter);
                }
                else
                {
                    subscription = new WeakBusEventSubscription<TBusEvent>(subscriptionToken, deliveryAction, eventFilter);
                }

                currentSubscriptions.Add(new SubscriptionItem(proxy, subscription));

                return subscriptionToken;
            }
        }

        private void RemoveSubscriptionInternal<TBusEvent>(BusEventSubscriptionToken subscriptionToken)
                where TBusEvent : class, IBusEvent
        {
            if (subscriptionToken == null)
            {
                throw new ArgumentNullException("subscriptionToken");
            }

            lock (m_subscriptionsPadlock)
            {
                List<SubscriptionItem> currentSubscriptions;
                if (!m_subscriptions.TryGetValue(typeof(TBusEvent), out currentSubscriptions))
                    return;

                var currentlySubscribed = (from sub in currentSubscriptions
                                           where object.ReferenceEquals(sub.Subscription.SubscriptionToken, subscriptionToken)
                                           select sub).ToList();

                currentlySubscribed.ForEach(sub => currentSubscriptions.Remove(sub));
            }
        }

        private void PublishInternal<TBusEvent>(TBusEvent busEvent)
                where TBusEvent : class, IBusEvent
        {
            if (busEvent == null)
            {
                throw new ArgumentNullException("event");
            }

            List<SubscriptionItem> currentlySubscribed;
            lock (m_subscriptionsPadlock)
            {
                List<SubscriptionItem> currentSubscriptions;
                if (!m_subscriptions.TryGetValue(typeof(TBusEvent), out currentSubscriptions))
                {
                    return;
                }

                currentlySubscribed = (from sub in currentSubscriptions
                                       where sub.Subscription.ShouldAttemptDelivery(busEvent)
                                       select sub).ToList();
            }

            currentlySubscribed.ForEach(sub =>
            {
                try
                {
                    sub.Proxy.Deliver(busEvent, sub.Subscription);
                }
                catch (Exception e)
                {
                    Utilities.Logger.WriteError(e, "Error");
                }
            });
        }

        private void PublishAsyncInternal<TBusEvent>(TBusEvent busEvent, AsyncCallback callback) where TBusEvent : class, IBusEvent
        {
            Action publishAction = () => { PublishInternal<TBusEvent>(busEvent); };
            publishAction.BeginInvoke(callback, null);
        }
        #endregion
    }
}
