using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 记录点击的信息
    /// </summary>
    public class ClickInfoView
    {
        /// <summary>
        /// 点击时间
        /// </summary>
        public DateTime ClickTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作的次数
        /// </summary>
        public int Times { get; set; }
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool IsOperateSucceed { get; set; }
    }
}
