using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Common.Exts
{
    public static class EasyuiAdaptor
    {
        /// <summary>
        /// 返回结果转为Easyui DataGrid 数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="msg">ResultMsg<PageMsg<T>> 对象</param>
        /// <returns></returns>
        public static object ToGridResult<T>(this ResultMsg<PageMsg<T>> msg)
        {
            return new { total = msg.Data.Total, rows = msg.Data.Rows };
        }

        /// <summary>
        /// 返回结果转为Easyui Combobox 数据
        /// </summary>
        /// <param name="msg">ResultMsg<List<ComboResult>>对象</param>
        /// <returns></returns>
        public static List<object> ToComboBoxResult(this ResultMsg<List<ComboResult>> msg) {
            var list = new List<object>();
            foreach (var item in msg.Data)
                list.Add(new {id=item.Id,text=item.Text,queryText=item.QueryText,fastQueryText=item.FastQueryText });
            return list;
        }

        /// <summary>
        /// 返回结果转为Easyui Tree 数据
        /// </summary>
        /// <param name="msg">ResultMsg<List<TreeMsg>>对象</param>
        /// <returns></returns>
        public static List<object> ToTreeResult(this ResultMsg<List<TreeMsg>> msg)
        {
            var list = new List<object>();
            foreach (var item in msg.Data)
                list.Add(GetNode(item));
            return list;
        }

        private static object GetNode(TreeMsg node)
        {
            //TODO:
            return null;
        }
    }
}
