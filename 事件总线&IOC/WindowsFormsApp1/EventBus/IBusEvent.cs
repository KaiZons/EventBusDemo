namespace EventBus
{
    /// <summary>
    /// A BusEvent to be published/delivered by BusEventHub
    /// </summary>
    public interface IBusEvent
    {
        /// <summary>
        /// The sender of the bus event, or null if not supported by the bus event implementation.
        /// </summary>
        object Sender { get; }
    }
}
