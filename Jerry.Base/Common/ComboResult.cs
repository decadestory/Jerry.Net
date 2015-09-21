using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Common
{
    public class ComboMsg
    {
        /// <summary>
        /// 值
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 显示文字
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string QueryText { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public string FastQueryText { get; set; }
    }
}
