using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Common
{
    /// <summary>
    /// 返回消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultMsg<T>
    {
        /// <summary>
        /// 返回标志代码：0为成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public object ExtData { get; set; }
    }
}
