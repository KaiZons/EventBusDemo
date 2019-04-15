using System;

namespace EventBus
{
    /// <summary>
    /// Represents an active subscription to a bus event
    /// </summary>
    public sealed class BusEventSubscriptionToken : IDisposable
    {
        private WeakReference m_hub;
        private Type m_eventType;

        /// <summary>
        /// Initializes a new instance of the BusEventSubscriptionToken class.
        /// </summary>
        public BusEventSubscriptionToken(IBusEventHub hub, Type eventType)
        {
            if (hub == null)
            {
                throw new ArgumentNullException("hub");
            }

            if (!typeof(IBusEvent).IsAssignableFrom(eventType))
            {
                throw new ArgumentOutOfRangeException("eventType");
            }

            m_hub = new WeakReference(hub);
            m_eventType = eventType;
        }

        public void Dispose()
        {
            if (m_hub.IsAlive)
            {
                var hub = m_hub.Target as IBusEventHub;

                if (hub != null)
                {
                    var unsubscribeMethod = typeof(IBusEventHub).GetMethod("Unsubscribe", new Type[] { typeof(BusEventSubscriptionToken) });
                    unsubscribeMethod = unsubscribeMethod.MakeGenericMethod(m_eventType);
                    unsubscribeMethod.Invoke(hub, new object[] { this });
                }
            }

            GC.SuppressFinalize(this);
        }
    }   
}
