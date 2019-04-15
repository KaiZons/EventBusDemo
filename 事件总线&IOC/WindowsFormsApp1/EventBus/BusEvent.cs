namespace EventBus
{
    /// <summary>
    /// Generic bus event with user specified content
    /// </summary>
    /// <typeparam name="TContent">Content type to store</typeparam>
    public class BusEvent<TContent> : BusEventBase
    {
        /// <summary>
        /// Contents of the event
        /// </summary>
        public TContent Content { get; protected set; }

        /// <summary>
        /// Create a new instance of the BusEvent class.
        /// </summary>
        /// <param name="sender">Event sender (usually "this")</param>
        /// <param name="content">Contents of the event</param>
        public BusEvent(object sender, TContent content)
            : base(sender)
        {
            Content = content;
        }
    }
}
