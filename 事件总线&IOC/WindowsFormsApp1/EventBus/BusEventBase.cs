using System;

namespace EventBus
{
    /// <summary>
    /// Base class for bus events that provides weak refrence storage of the sender
    /// </summary>
    public abstract class BusEventBase : IBusEvent
    {
        /// <summary>
        /// Store a WeakReference to the sender just in case anyone is daft enough to
        /// keep the event around and prevent the sender from being collected.
        /// </summary>
        private WeakReference m_sender;
        public object Sender
        {
            get
            {
                return (m_sender == null) ? null : m_sender.Target;
            }
        }

        /// <summary>
        /// Initializes a new instance of the BusEventBase class.
        /// </summary>
        /// <param name="sender">event sender (usually "this")</param>
        public BusEventBase(object sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }

            m_sender = new WeakReference(sender);
        }
    }
}
