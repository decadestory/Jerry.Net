using Jerry.Base.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace Jerry.Base.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// 扩展方法：根据值获取描述
        /// </summary>
        /// <returns></returns>
        public static string GetDesc<T>(this int value)
        {
            var list = Enum.GetValues(typeof (T));
            foreach (var item in list.Cast<Enum>().Where(item => item.GetHashCode() == value))
                return item.Description();
            return "";
        }

        /// <summary>
        /// 静态方法：获取枚举键值列表(可用于下拉框)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<ComboResult> EnumToList<T>(bool addAll=false)
        {
            var list = (from Enum type in Enum.GetValues(typeof (T))
                        select new ComboResult { Id = type.GetHashCode(), Text = type.Description() }).ToList();
            if (addAll) list.Insert(0, new ComboResult { Id = 0,Text = "所有"});
            return list;
        }

        /// <summary>
        /// 私有方法：获取当前枚举的描述
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private static string Description(this Enum temp)
        {
            var enumType = temp.GetType();
            try
            {
                var fieldInfo = enumType.GetField(temp.ToString());
                var attr =
                    Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as
                        DescriptionAttribute;
                return attr != null ? attr.Description : "";
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}
