using System;

namespace EventBus
{
    /// <summary>
    /// Basic "cancellable" generic bus event
    /// </summary>
    /// <typeparam name="TContent">Content type to store</typeparam>
    public class CancellableBusEvent<TContent> : BusEventBase
    {
        /// <summary>
        /// Cancel action
        /// </summary>
        public Action Cancel { get; protected set; }

        /// <summary>
        /// Contents of the event
        /// </summary>
        public TContent Content { get; protected set; }

        /// <summary>
        /// Create a new instance of the CancellableBusEvent class.
        /// </summary>
        /// <param name="sender">Bus event sender (usually "this")</param>
        /// <param name="content">Contents of the event</param>
        /// <param name="cancelAction">Action to call for cancellation</param>
        public CancellableBusEvent(object sender, TContent content, Action cancelAction)
            : base(sender)
        {
            if (cancelAction == null)
            {
                throw new ArgumentNullException("cancelAction");
            }

            Content = content;
            Cancel = cancelAction;
        }
    }
}
