using EventPublishers;
using Model;
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
using Utilities;

namespace Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            TestButtonClickEventPublisher.PublishMainFormButtonClickBusEvent(this,
                GetClickInfo(),
                (isSucceed, clickInfo) =>
                {
                    clickInfo.IsOperateSucceed = isSucceed;
                    if (clickInfo.IsOperateSucceed)
                    {
                        MessageBox.Show("响应成功！");
                    }
                });
        }

        private ClickInfoView GetClickInfo()
        {
            return new ClickInfoView()
            {
                ClickTime = DateTime.Now,
                Operator = "zhoukaikai",
                Times = 1,
                IsOperateSucceed = false
            };
        }
        
    }
}
