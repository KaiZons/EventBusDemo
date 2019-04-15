using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageControl
{
    public sealed class MessageCenter
    {
        #region Singleton Members
        public static readonly MessageCenter Instance = new MessageCenter();

        static MessageCenter()
        {
        }

        private MessageCenter()
        {
            messages = new Queue<DataType>();
        }
        #endregion

        Queue<DataType> messages;
        private object locker = new object();

        public event EventHandler<MessageEventArgs> MessageAdd;

        public void AddMessage(DataType message)
        {
            lock (locker)
            {
                messages.Enqueue(message);
            }

            MessageEventArgs e = new MessageEventArgs(message);
            OnAddMessage(e);
        }

        private void OnAddMessage(MessageEventArgs e)
        {
            e.Raise<MessageEventArgs>(this, ref MessageAdd);

            lock (locker)
            {
                if (messages.Count > 0)
                {
                    messages.Dequeue();
                }
            }
        }

    }

    /// <summary>
    /// 触发线程安全的事件
    /// </summary>
    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref EventHandler<TEventArgs> eventDelegate)
             where TEventArgs : EventArgs
        {
            // 拷贝一个引用，线程安全的
            EventHandler<TEventArgs> temp = Interlocked.CompareExchange(ref eventDelegate, null, null);
            // 通知事件发生了
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }

    /// <summary>
    /// 消息事件的参数
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        private readonly DataType dataObjectType;

        public DataType DataObjectType
        {
            get { return dataObjectType; }
        }


        public MessageEventArgs(DataType dataObjectType)
        {
            this.dataObjectType = dataObjectType;
        }

    }

    public static class ErrorLogRecorder
    {
        public static void WriteLog(string filePath, string errorMessage)
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write))
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.GetEncoding("gb2312")))
                    {
                        sw.WriteLine(DateTime.Now.ToLocalTime().ToString() + errorMessage);
                    }
                }
            }
            catch
            {
            }
        }
    }
    
    public enum DataType
    {
        TextValue = 0
    }
}
