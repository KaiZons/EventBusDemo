using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageControl
{
    
    public partial class Form1 : Form
    {
        public event EventHandler NowDownloadProgressEvent;
        public Form1()
        {
            InitializeComponent();
            //MessageCenter.Instance.MessageAdd += Instance_MessageAdd;//订阅（主线程订阅）

            NowDownloadProgressEvent += Instance_MessageAdd;
        }

        private void Instance_MessageAdd(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => { m_richTextBox.Text = GenericCacheTest.Show();}) );
        }

        private void m_asyncButton_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                //Thread.Sleep(1000);
                ////MessageCenter.Instance.MessageAdd.Invoke(null, null);
                //MessageCenter.Instance.AddMessage(DataType.TextValue);//发布（子线程发布）

                //int progress = (int)(((float)dataSize * packetNumber) / fileStream.Length * 100);
                NowDownloadProgressEvent.Invoke(null, new EventArgs());
            });
        }
        
    }
}
