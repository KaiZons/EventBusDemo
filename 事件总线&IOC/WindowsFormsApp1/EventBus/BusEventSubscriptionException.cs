using System;

namespace EventBus
{
    public class BusEventSubscriptionException : Exception
    {
        private const string ErrorText = "Unable to add subscription for {0} : {1}";

        public BusEventSubscriptionException(Type busEventType, string reason)
            : base(String.Format(ErrorText, busEventType, reason))
        {

        }

        public BusEventSubscriptionException(Type busEventType, string reason, Exception innerException)
            : base(String.Format(ErrorText, busEventType, reason), innerException)
        {

        }
    }
}
