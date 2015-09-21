using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Jerry.Base.Common.Exts
{
    public static class EasyuiAdaptor
    {
        private static List<TreeMsg> _csrc;


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
        /// <param name="msg">ResultMsg<List/><ComboResult/>>对象</param>
        /// <returns></returns>
        public static List<object> ToComboBoxResult(this ResultMsg<List<ComboMsg>> msg)
        {
            var list = new List<object>();
            foreach (var item in msg.Data)
                list.Add(new { id = item.Id, text = item.Text, queryText = item.QueryText, fastQueryText = item.FastQueryText });
            return list;
        }

        /// <summary>
        /// 返回结果转为Easyui Tree 数据
        /// </summary>
        /// <param name="msg">ResultMsg<List/><TreeMsg/>>对象</param>
        /// <returns></returns>
        public static List<ETreeMsg> ToTreeResult(this ResultMsg<List<TreeMsg>> msg)
        {
            _csrc = _csrc.Where(t => t.ParentId != 0).ToList();
            return msg.Data.Select(GetNode).ToList();
        }

        private static ETreeMsg GetNode(TreeMsg node)
        {
            var children = _csrc.Where(t => t.ParentId == node.Id).ToList();
            if (!children.Any()) return new ETreeMsg { id = node.Id, text = node.Text,ischecked = node.Checked,state = node.State};
            var tnode = new ETreeMsg { id = node.Id, text = node.Text, ischecked = node.Checked, state = node.State };
            foreach (dynamic cnode in children.Select(child => GetNode(child)).Where(cnode => cnode.id != node.Id )) tnode.children.Add(cnode);
            return tnode;
        }
    }

    public class ETreeMsg
    {
        public int id { get; set; }
        public string text { get; set; }
        public IList<ETreeMsg> children { get; set; }
        public int state { get; set; }

        public bool ischecked { get; set; }
    }

}
