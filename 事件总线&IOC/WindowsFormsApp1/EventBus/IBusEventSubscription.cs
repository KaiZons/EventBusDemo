namespace EventBus
{
    /// <summary>
    /// Represents a BusEvent subscription
    /// </summary>
    public interface IBusEventSubscription
    {
        /// <summary>
        /// Token returned to the subscribed to reference this subscription
        /// </summary>
        BusEventSubscriptionToken SubscriptionToken { get; }

        /// <summary>
        /// Whether delivery should be attempted.
        /// </summary>
        /// <param name="busEvent">BusEvent that may potentially be delivered.</param>
        /// <returns>True - ok to send, False - should not attempt to send</returns>
        bool ShouldAttemptDelivery(IBusEvent busEvent);

        /// <summary>
        /// Deliver the BusEvent
        /// </summary>
        /// <param name="busEvent">BusEvent to deliver</param>
        void Deliver(IBusEvent busEvent);
    }

}
