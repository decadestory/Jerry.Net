using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Common
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public class PageMsg<TData>
    {
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 返回分页数据
        /// </summary>
        public IList<TData> Rows { get; set; }
    }
}
