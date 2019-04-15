namespace EventBus
{
    /// <summary>
    /// Implement this interface if you want to subscribe some bus events after your assembly loaded.
    /// </summary>
    public interface IBusPeer
    {
        void Initialize();
    }
}
