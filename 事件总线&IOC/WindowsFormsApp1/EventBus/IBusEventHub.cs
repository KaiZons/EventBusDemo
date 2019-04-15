using System;

namespace EventBus
{
    /// <summary>
    /// BusEvent hub responsible for taking subscriptions/publications and delivering of bus events.
    /// </summary>
    public interface IBusEventHub
    {
        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action.
        /// All references are held with WeakReferences
        /// 
        /// All bus events of this type will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction) where TBusEvent : class, IBusEvent;

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action.
        /// Bus events will be delivered via the specified proxy.
        /// All references (apart from the proxy) are held with WeakReferences
        /// 
        /// All bus events of this type will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="proxy">Proxy to use when delivering the bus events</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, IBusEventProxy proxy) where TBusEvent : class, IBusEvent;

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action.
        /// 
        /// All bus events of this type will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <param name="useStrongReferences">Use strong references to destination and deliveryAction </param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, bool useStrongReferences) where TBusEvent : class, IBusEvent;

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
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, bool useStrongReferences, IBusEventProxy proxy) where TBusEvent : class, IBusEvent;

        /// <summary>
        /// Subscribe to a bus event type with the given destination and delivery action with the given filter.
        /// All references are held with WeakReferences
        /// 
        /// Only bus events that "pass" the filter will be delivered.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="deliveryAction">Action to invoke when bus event is delivered</param>
        /// <returns>BusEventSubscriptionToken used to unsubscribing</returns>
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter) where TBusEvent : class, IBusEvent;

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
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter, IBusEventProxy proxy) where TBusEvent : class, IBusEvent;

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
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter, bool useStrongReferences) where TBusEvent : class, IBusEvent;

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
        BusEventSubscriptionToken Subscribe<TBusEvent>(Action<TBusEvent> deliveryAction, Func<TBusEvent, bool> eventFilter, bool useStrongReferences, IBusEventProxy proxy) where TBusEvent : class, IBusEvent;

        /// <summary>
        /// Unsubscribe from a particular bus event type.
        /// 
        /// Does not throw an exception if the subscription is not found.
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="subscriptionToken">Subscription token received from Subscribe</param>
        void Unsubscribe<TBusEvent>(BusEventSubscriptionToken subscriptionToken) where TBusEvent : class, IBusEvent;

        /// <summary>
        /// Publish a bus event to any subscribers
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="event">Bus event to deliver</param>
        void Publish<TBusEvent>(TBusEvent busEvent) where TBusEvent : class, IBusEvent;

        /// <summary>
        /// Publish a bus event to any subscribers asynchronously
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="event">Bus event to deliver</param>
        void PublishAsync<TBusEvent>(TBusEvent busEvent) where TBusEvent : class, IBusEvent;

        /// <summary>
        /// Publish a bus event to any subscribers asynchronously
        /// </summary>
        /// <typeparam name="TBusEvent">Type of bus event</typeparam>
        /// <param name="event">Bus event to deliver</param>
        /// <param name="callback">AsyncCallback called on completion</param>
        void PublishAsync<TBusEvent>(TBusEvent busEvent, AsyncCallback callback) where TBusEvent : class, IBusEvent;
    }
}
