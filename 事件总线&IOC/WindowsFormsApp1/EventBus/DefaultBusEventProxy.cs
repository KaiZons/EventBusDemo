namespace EventBus
{
    /// <summary>
    /// Default "pass through" proxy.
    /// 
    /// Does nothing other than deliver the bus event.
    /// </summary>
    public sealed class DefaultBusEventProxy : IBusEventProxy
    {
        private static readonly DefaultBusEventProxy m_instance = new DefaultBusEventProxy();

        static DefaultBusEventProxy()
        {
        }

        /// <summary>
        /// Singleton instance of the proxy.
        /// </summary>
        public static DefaultBusEventProxy Instance
        {
            get
            {
                return m_instance;
            }
        }

        private DefaultBusEventProxy()
        {
        }

        public void Deliver(IBusEvent busEvent, IBusEventSubscription subscription)
        {
            subscription.Deliver(busEvent);
        }
    }
}
