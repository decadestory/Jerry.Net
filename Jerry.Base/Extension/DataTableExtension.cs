using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jerry.Base.Extension
{
    public static class  DataTableExtension
    {
        /// <summary>
        /// 将dt2合并到dt1中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dt2"></param>
        /// <returns>返回dt1</returns>
        public static DataTable AppendTable(this DataTable dt,DataTable dt2)
        {
            var obj = new object[dt.Columns.Count];
            foreach (DataRow dr in dt2.Rows)
            {
                dr.ItemArray.CopyTo(obj, 0);
                dt.Rows.Add(obj);
            }
            return dt;
        }


        /// <summary>
        /// DataTable转成List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table)
        {
            var list = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var t = Activator.CreateInstance<T>();
                var propertypes = t.GetType().GetProperties();
                foreach (var pro in propertypes)
                {
                    var tempName = pro.Name;
                    if (!table.Columns.Contains(tempName)) continue;
                    var value = row[tempName];
                    if (value is DBNull)
                    {
                        value = null;
                    }
                    pro.SetValue(t, value, null);
                }
                list.Add(t);
            }
            return list;
        }


    }
}
