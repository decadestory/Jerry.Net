using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Config
{
    public class Configs
    {
        /// <summary>
        /// 根据name获取连接字符
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetConnSetting(string name)
        {
            var temp = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            return string.IsNullOrEmpty(temp) ? "" : temp;
        }

        /// <summary>
        /// 根据name获取value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetAppSetting(string name)
        {
            var temp = ConfigurationManager.AppSettings[name];
            return string.IsNullOrEmpty(temp) ? "" : temp;
        }

    }
}
