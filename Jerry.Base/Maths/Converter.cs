using System;

namespace Jerry.Base.Maths
{
    public static class Converter
    {
        /// <summary>
        ///值类型保留小数
        /// </summary>
        /// <param name="val"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string ToStd(this object val, int len = 2)
        {
            try
            {
                var output = Convert.ToDecimal(val).ToString("f" + len);
                return output;
            }
            catch (Exception)
            {
                return val + "";
            }
        }
    }
}