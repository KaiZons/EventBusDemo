using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views
{
    public partial class SubForm : Form
    {
        private ClickInfoView m_clickInfo;
        public SubForm(ClickInfoView clickInfo)
        {
            InitializeComponent();
            m_clickInfo = clickInfo;
        }

        private void OnSubFormLoad(object sender, EventArgs e)
        {
            if (m_clickInfo != null)
            {
                m_text.Text = $"激活时间：{m_clickInfo.ClickTime}\r\n" +
                    $"激活人：{m_clickInfo.Operator}\r\n";
            }
        }
    }
}
