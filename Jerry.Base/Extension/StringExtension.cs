using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using Jerry.Base.Common;

namespace Jerry.Base.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 扩展方法:将字符串按长度切割
        /// </summary>
        /// <param name="oldStr"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Cut(this string oldStr, int len = 10)
        {
            var oldStrLen = oldStr.Length;
            if (oldStrLen <= len)
            {
                return oldStr;
            }
            else
            {
                return oldStr.Substring(0, len - 3) + "...";
            }
        }

        #region   字符串长度区分中英文截取
        /// <summary>   
        /// 截取文本，区分中英文字符，中文算两个长度，英文算一个长度
        /// </summary>
        /// <param name="str">待截取的字符串</param>
        /// <param name="length">需计算长度的字符串</param>
        /// <returns>string</returns>
        public static string Cuts(this string str, int length=10)
        {
            string temp = str;
            int j = 0;
            int k = 0;
            for (int i = 0; i < temp.Length; i++)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
            {
                j= Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+")?j+2:j+1; 
                k=j <= length?k + 1:k;
                if (j > length) return temp.Substring(0, k) + "...";
            }
            return temp;
        }
        #endregion


        /// <summary>
        /// 扩展方法：将字符串逆转
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static string Reverse(this string oldStr)
        {
            char[] temp = oldStr.ToCharArray();
            Array.Reverse(temp);
            return new string(temp);
        }

        /// <summary>
        /// 扩展方法:手机号转化成189****6547形式
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns>'Error': 号码格式出错</returns>
        public static string GetHidePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 11)
            {
                return phoneNumber.Substring(0, 3) + "****" + phoneNumber.Substring(7, 4);
            }
            else
            {
                return "Error";
            }
        }

        #region 字符串验证

        /// <summary>
        /// 扩展方法:判断字符串是否只由数字组成
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static bool IsNumber(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^[0-9]+$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否只由数字或字母组成
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static bool IsNumberOrLetter(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^[A-Za-z0-9]+$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否只由数字或字母或汉字组成
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static bool IsNumberOrLetterOrChinese(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"[a-zA-Z0-9\u4e00-\u9fa5]{1,50}");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否只由汉字组成
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static bool IsChinese(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否是Email
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static bool IsEmail(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否是身份证号
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static bool IsIDCard(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"^\d{17}(\d|x)$");
        }

        /// <summary>
        /// 扩展方法:判断字符串是否是手机号
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static bool IsMobile(this string oldStr)
        {
            return Regex.IsMatch(oldStr, @"(^189\d{8}$)|(^13\d{9}$)|(^15\d{9}$)");
        }

        /// <summary>
        /// 名称：CheckPassPortChina
        /// 功能：检查中国公民身份证是否正确
        /// </summary>
        /// <param name="cid">需检查的身份证号码</param>
        /// <returns>返回由省市，生日，性别组成的字符串</returns>
        public static ResultMsg<bool> IsAbsoluteIdCard(this string cid)
        {
            var aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            var rg = new Regex(@"^\d{17}(\d|x)$");
            Match mc = rg.Match(cid);
            if (!mc.Success)
            {
                return new ResultMsg<bool> { Code = -1, Data = false, ErrMsg = "身份证格式" }; ;
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null)
            {
                return new ResultMsg<bool> { Code = -1, Data = false, ErrMsg = "非法地区" };
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
            }
            catch
            {
                return new ResultMsg<bool> { Code = -1, Data = false, ErrMsg = "非法生日" }; 
            }
            for (int i = 17; i >= 0; i--)
            {
                iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cid[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
            }
            if (iSum % 11 != 1)
            {
                return new ResultMsg<bool> { Code = -1, Data = false, ErrMsg = "非法证号" }; 
            }

            var data = aCity[int.Parse(cid.Substring(0, 2))] + "," + cid.Substring(6, 4) + "-" + cid.Substring(10, 2) +
                       "-" + cid.Substring(12, 2) + "," + (int.Parse(cid.Substring(16, 1))%2 == 1 ? "男" : "女");
            return new ResultMsg<bool> { Code = 0, Data = true, ErrMsg = data }; 
        }
        #endregion
        
        #region 字符串加密
        /// <summary>
       /// 扩展方法:字符串加密
       /// </summary>
       /// <param name="oldStr"></param>
       /// <returns>加密后的字符串</returns>
        public static string CreatePassword(this string oldStr)
        {
            /*NOTE:
             * 一个密码字符串由数字和母组成
             * STEP1: 产生一个key,范围在11-99之间
             * STEP2: 移位值=(字符串长度+key)%(key个十位相加值)
             * STEP3: 数字=数字左移移位值位，字母=字母右移位值位
             * STEP4: key转成十六进制(2位)加在结果后面
            */
            int key,displacement;
            repeat: key = new Random().Next(11,99);
            displacement = (oldStr.Length + key) % (key % 10 + key / 10);
            if (displacement == 0) goto repeat;
            string result = "";

            char[] items = oldStr.ToCharArray();
            foreach(var i in items) 
            {
                if (i-48 >= 0 && i-48 <= 9)
                {
                    if((i-displacement)>='0')
                    {
                        result += (char)(i-displacement);
                    }
                    else
                    {
                        result += (char)(58 - (48 - (i - displacement)));
                    }
                }
                else if(i >= 'a' && i <='z')
                {
                    result += (char)((i - 97 + displacement) % 26 + 97);
                }
                else if (i >= 'A' && i <= 'Z')
                {
                    result += (char)((i - 65 + displacement) % 26 + 65);
                }
            }
            return result + key.ToString("X2");
        }

        /// <summary>
        /// 扩展方法:字符串解密
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns>解密后的字符串</returns>
        public static string RemovePassword(this string oldStr)
        {
            string handleStr = oldStr.Substring(0, oldStr.Length - 2);   
            int key = Int32.Parse(oldStr.Substring(oldStr.Length - 2), System.Globalization.NumberStyles.HexNumber);
            int dpt = (handleStr.Length + key) % (key % 10 + key / 10);
            char[] items = handleStr.ToCharArray();
            string result = "";
            foreach(var i in items)
            {
                if (i-48 >= 0 && i-48 <= 9)
                {
                    if (i + dpt <= '9')
                        result += (char)(i + dpt);
                    else
                        result += (char)(48 + ((i + dpt) - 58));
                }
                else if(i >= 'a' && i <='z')
                {
                    if (i - dpt >= 'a')
                    {
                        result += (char)(i - dpt);
                    }
                    else
                    {
                        result += (char)(123 - Math.Abs(97 - (i - dpt)));
                    }
                }
                else if (i >= 'A' && i <= 'Z')
                {
                    if (i - dpt >= 'A')
                    {
                        result += (char)(i - dpt);
                    }
                    else
                    {
                        result += (char)(81 - Math.Abs(65 - (i - dpt)));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 扩展方法:数字加密算法
        /// Step1: oldStr(oldStr小于等于9位) * key各位之和(key小于等于99)
        /// Step2: 再将其与(key*key)异或
        /// Step3: 再将结果倒序输出
        /// 例如: oldStr="12345678" ,key=56
        /// Step1: 12345678 * 11 = 135802458
        /// Step2: 135802458^(56*56)=135799322
        /// 结果: 223997531
        /// </summary>
        /// <param name="oldStr">最大9位数</param>
        /// <param name="key">两位数密钥</param>
        /// <returns></returns>
        public static string ToPwd(this string oldStr,int key)
        {
            if (!oldStr.IsNumber()) {
                return "Inviad String";
            }

            if (oldStr.Length > 10)
            {
                return "Too Long";
            }

            int keyer = key / 10 + key % 10;
            string step1 = (Convert.ToInt64(oldStr)*keyer).ToString();
            string step2 = (Convert.ToInt64(step1) ^ (key * key)).ToString();
            string result = step2.Reverse();
            return result;
        }

        /// <summary>
        /// 扩展方法:数字解密密算法
        /// </summary>
        /// <param name="oldStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DePwd(this string oldStr, int key)
        {
            int keyer = key / 10 + key % 10;
            string step1 = oldStr.Reverse();
            string step2 = (Convert.ToInt64(step1) ^ (key * key)).ToString();
            string result = (Convert.ToInt64(step2) / keyer).ToString();
            return result;
        }

        /// <summary>
        /// 扩展方法:将字符以MD5方式加密
        /// </summary>
        /// <param name="oldStr">加密字符串</param>
        /// <returns>加密结果</returns>
        public static string GetMD5(this string input ,string str="")
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString()+str;
        }
        #endregion

        /// <summary>
        /// 扩展方法:base64加密,可用于Url加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64Str(this string str)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(str)).Replace("+", "%2B");
        }

        /// <summary>
        /// 扩展方法:base64解密，可用于Url解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeBase64Str(this string str)
        {
            return System.Text.Encoding.Default.GetString(Convert.FromBase64String(str.Replace("%2B", "+")));
        }

        /// <summary>
        /// 扩展方法:替换字符串中的html代码 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceHtmlString(this string str)
        {
            return Regex.Replace(str, @"<[^>]+>", "");
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="code">加密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DesEncrypt(this string code, string key)
        {
            string iv = key;
            return DesEncrypt(code, key, iv);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="code">加密字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public static string DesEncrypt(string code, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.Default.GetBytes(code);
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ms.Dispose();
                cs.Dispose();
                //ret.ToString();
                return ret.ToString();
            }
            catch (Exception)
            {

                return code;
            }

        }

        /// <summary>
        /// DES解密,解密失败返回源串
        /// </summary>
        /// <param name="code">解密字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesDecrypt(this string code, string key)
        {
            string iv = key;
            return DesDecrypt(code, key, iv);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="code">解密字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public static string DesDecrypt(string code, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[code.Length / 2];
                for (int x = 0; x < code.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(code.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(iv);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Dispose();
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception)
            {

                return code;
            }

        }
        /*----------------------------------------------方法分隔线----------------------------------------------*/
    }
}