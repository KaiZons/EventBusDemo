namespace EventBus
{
    /// <summary>
    /// BusEvent proxy definition.
    /// 
    /// A BusEvent proxy can be used to intercept/alter bus events and/or
    /// marshall delivery actions onto a particular thread.
    /// </summary>
    public interface IBusEventProxy
    {
        void Deliver(IBusEvent busEvent, IBusEventSubscription subscription);
    }
}
