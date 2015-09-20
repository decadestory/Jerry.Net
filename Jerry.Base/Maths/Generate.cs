using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Maths
{
    public class Generate
    {
        /// <summary>
        /// 生成一串随机数字,默认6位，最长9位
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns>string</returns>
        public static int GenerateRandomNumber(int length = 6)
        {
            var start = new StringBuilder("1");
            var end = new StringBuilder("9");

            if (length > 9) return -1;

            for (var i = 1; i < length; i++)
            {
                start.Append("0");
                end.Append("9");
            }

            var vmin = Convert.ToInt32(start.ToString());
            var vmax = Convert.ToInt32(end.ToString());

            return new Random().Next(vmin, vmax);
        }

        /// <summary>
        /// 生成一个随机数
        /// </summary>
        /// <returns></returns>
        public static int OneRandomNumber()
        {
            var rand = new Random();
            var i = rand.Next(10);
            return i;
        }
    }
}
