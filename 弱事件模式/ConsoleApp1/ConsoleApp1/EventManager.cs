using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleApp1
{
    public class EventManager : WeakEventManager
    {
        private static EventManager CurrentManager
        {
            get
            {
                EventManager manager = (EventManager)GetCurrentManager(typeof(EventManager));

                if (manager == null)
                {
                    manager = new EventManager();
                    SetCurrentManager(typeof(EventManager), manager);
                }

                return manager;
            }
        }


        public static void AddListener(EventSource source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(EventSource source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        protected override void StartListening(object source)
        {
            ((EventSource)source).EventHandlerEvent += DeliverEvent;
        }

        protected override void StopListening(object source)
        {
            ((EventSource)source).EventHandlerEvent -= DeliverEvent;
        }
    }
}
