using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Maths
{
    public class BigInteger
    {
        public string valuestr;
        public char[] value = new char[101];
        public BigInteger()
        {
            this.valuestr = "0";  //默认值为0
            value[0] = '+';
            for (int i = 1; i < value.Length; i++)
                value[i] = '0';
        }
        public BigInteger(string valuestr)
        {
            this.valuestr = valuestr;
            for (int i = 0; i < value.Length; i++)
                value[i] = '0';
            Str_to_Value(valuestr);  //数字的字符串形式转换为数组
        }
        public char[] Str_to_Value(string valuestr)
        {
            char[] value1 = valuestr.ToCharArray();
            if ((value1[0] != '+') && (value1[0] != '-'))
            {
                value[0] = '+';  //0或整数时，value[0]存放符号为‘+’
                for (int j = 0; j < value1.Length; j++)
                    value[value.Length - 1 - j] = value1[value1.Length - 1 - j];
            }
            else
            {
                value[0] = value1[0];   //负数时，value[0]存放符号为‘-’
                for (int j = 1; j < value1.Length; j++)
                    value[value.Length - j] = value1[value1.Length - j];
            }
            return value;
        }
        public string Value_to_Str()  //Value[]转换成字符串方便显示
        {
            char start = value[0];
            string str = "";
            foreach (char c in value)
            {
                str += c.ToString();
            }
            str = str.TrimStart('-').TrimStart('+').TrimStart('0');  //去除数字前面的0
            if (start == '-')    //负数保留负号
                str = start.ToString() + str;
            else if (str == "")
                str += "0";
            return str;
        }
        public static bool operator ==(BigInteger b1, BigInteger b2)	 // == 重载
        {
            if (b1.value[0] != b2.value[0])
                return false;
            else
            {
                for (int i = b1.value.Length - 1; i > 1; i--)
                {
                    int n1 = int.Parse(b1.value[i].ToString());
                    int n2 = int.Parse(b2.value[i].ToString());
                    if (n1 != n2) return false;     //只要有一位数字不等，则它们肯定不等                           
                }
            }
            return true;
        }
        public static bool operator !=(BigInteger b1, BigInteger b2)	 // != 重载
        {
            if (b1 == b2)
                return false;
            else
                return true;
        }
        public static bool operator >(BigInteger b1, BigInteger b2)	 // > 重载
        {
            if (b1.value[0] != b2.value[0]) //异号时
            {
                if (b1.value[0] == '+')
                    return true;
                else
                    return false;
            }
            else
            {
                if (b1.value[0] == '+')  //同为正数
                    for (int i = 1; i < b1.value.Length; i++)
                    {
                        int n1 = int.Parse(b1.value[i].ToString());
                        int n2 = int.Parse(b2.value[i].ToString());
                        if (n1 != n2)
                        {
                            if (n1 > n2)
                                return true;
                            else
                                return false;
                        }
                    }
                else  //同为负数
                    for (int i = 1; i < b1.value.Length; i++)
                    {
                        int n1 = int.Parse(b1.value[i].ToString());
                        int n2 = int.Parse(b2.value[i].ToString());
                        if (n1 != n2)
                        {
                            if (n1 > n2)
                                return false;
                            else
                                return true;
                        }
                    }
            }
            return false;
        }
        public static bool operator <(BigInteger b1, BigInteger b2)	 // < 重载
        {
            if (b1 == b2)
                return false;
            else if (b1 > b2)
                return false;
            else
                return true;
        }
        public static BigInteger operator +(BigInteger b1, BigInteger b2)  //  + 重载
        {
            BigInteger b3 = new BigInteger();
            BigInteger b11 = new BigInteger();
            BigInteger b22 = new BigInteger();
            BigInteger temp = new BigInteger();
            for (int i = 0; i < b11.value.Length; i++)
            {
                b11.value[i] = b1.value[i];
                b22.value[i] = b2.value[i];
            }
            int carry = 0;
            if ((b1.value[0] == '+' && b2.value[0] == '+') || (b1.value[0] == '-' && b2.value[0] == '-'))  //同号
            {
                for (int i = b1.value.Length - 1; i > 1; i--)
                {
                    int n1 = int.Parse(b1.value[i].ToString());
                    int n2 = int.Parse(b2.value[i].ToString());
                    char[] a = ((n1 + n2 + carry) % 10).ToString().ToCharArray();
                    b3.value[i] = a[a.Length - 1];
                    if (n1 + n2 >= 10)  //有进位
                        carry = 1;
                    else
                        carry = 0;
                }
                b3.value[0] = b1.value[0];
            }
            else  //异号
            {
                b11.value[0] = '+';
                b22.value[0] = '+';
                if (b11 == b22)
                {
                    b3.value[0] = '+';
                    for (int i = 1; i < b3.value.Length; i++)
                        b3.value[i] = '0';
                }
                else
                {
                    if (b11 < b22)  //b1的绝对值小于b2的绝对值
                    {
                        if (b1.value[0] == '+')
                        {
                            b3.value[0] = '-';
                        }
                        else
                        {
                            b3.value[0] = '+';
                        }
                        temp = b22;
                        b22 = b11;
                        b11 = temp;
                    }
                    else  //b1的绝对值大于b2的绝对值
                    {
                        if (b1.value[0] == '+')
                            b3.value[0] = '+';
                        else
                            b3.value[0] = '-';
                    }
                    carry = 0;
                    for (int i = b1.value.Length - 1; i > 1; i--) //绝对值大的数减绝对值小的数
                    {
                        char[] a;
                        int n1 = int.Parse(b11.value[i].ToString());
                        int n2 = int.Parse(b22.value[i].ToString());
                        if (n1 + carry >= n2)  //够减，无借位
                        {
                            a = (n1 + carry - n2).ToString().ToCharArray();
                            carry = 0;
                        }
                        else    //不够减，要借位
                        {
                            a = (n1 + carry + 10 - n2).ToString().ToCharArray();
                            carry = -1;
                        }
                        b3.value[i] = a[a.Length - 1];
                    }

                }
            }
            b3.valuestr = b3.Value_to_Str();
            return b3;
        }
        public static BigInteger operator -(BigInteger b1, BigInteger b2)	 //  - 重载
        {
            BigInteger b3 = new BigInteger();
            BigInteger b11 = new BigInteger();
            BigInteger b22 = new BigInteger();
            for (int i = 0; i < b11.value.Length; i++)
            {
                b11.value[i] = b1.value[i];
                b22.value[i] = b2.value[i];
            }
            if (b11 == b22)  //被减数和减数相等
            {
                b3.value[0] = '+';
                for (int i = 1; i < b3.value.Length; i++)
                    b3.value[i] = '0';
            }
            else
            {
                if (b22.value[0] == '+')
                    b22.value[0] = '-';
                else b22.value[0] = '+';
                b3 = b11 + b22;
            }
            return b3;
        }

        public static BigInteger operator *(BigInteger b1, BigInteger b2)	 //  * 重载
        {
            BigInteger b3 = new BigInteger();
            BigInteger b11 = new BigInteger();
            BigInteger b22 = new BigInteger();
            for (int i = 0; i < b11.value.Length; i++)
            {
                b11.value[i] = b1.value[i];
                b22.value[i] = b2.value[i];
            }
            if ((b11 == "0") || (b22 == "0"))  //只要一个数为0，结果为0
            {
                b3.value[0] = '+';
                for (int i = 1; i < b3.value.Length; i++)
                    b3.value[i] = '0';
            }
            else
            {
                for (int i = b11.value.Length - 1; i > 1; i--)
                {
                    for (int j = 0; j < int.Parse(b11.value[i].ToString()); j++)
                    {
                        b3 += b22;
                    }
                    for (int i1 = 1; i1 < b11.value.Length - 1; i1++) //b22左移一位相当于乘以10
                        b22.value[i1] = b22.value[i1 + 1];
                    b22.value[b11.value.Length - 1] = '0';
                }
            }
            if (((b1.value[0] == '+') && (b2.value[0] == '+')) || ((b1.value[0] == '-') && (b2.value[0] == '-')))  //同号为正
                b3.value[0] = '+';
            else   //异号为负
                b3.value[0] = '-';
            b3.valuestr = b3.Value_to_Str();
            return b3;
        }

        public static BigInteger operator /(BigInteger b1, BigInteger b2)	 //  / 重载
        {
            BigInteger b3 = new BigInteger();
            BigInteger b11 = new BigInteger();
            BigInteger b22 = new BigInteger();
            for (int i = 0; i < b11.value.Length; i++)
            {
                b11.value[i] = b1.value[i];
                b22.value[i] = b2.value[i];
            }
            if ((b11 == "0") && (b22 != "0"))  //被除数为0且除数不为0
            {
                b3.value[0] = '+';
                for (int i = 1; i < b3.value.Length; i++)
                    b3.value[i] = '0';
            }
            else if (b22 == "0") //除数不能为0
            {
                Console.WriteLine("0 不能作为除数！");
                return null;
            }
            else
            {
                b11.value[0] = '+';  //b11、b22取绝对值
                b22.value[0] = '+';
                b11.valuestr = b11.Value_to_Str();
                b22.valuestr = b22.Value_to_Str();
                int tem = 0;
                while (b22.valuestr.Length < b11.valuestr.Length)//除数补成和被除数长度一样
                {
                    for (int i1 = 1; i1 < b11.value.Length - 1; i1++)
                        b22.value[i1] = b22.value[i1 + 1];
                    b22.value[b11.value.Length - 1] = '0';
                    b22.valuestr = b22.Value_to_Str();
                    tem++; //记录补位个数
                }
                for (int a = tem; a >= 0; a--)
                {
                    int b = 0;
                    while ((b11 - b22) > "0" || (b11 - b22) == "0")
                    {
                        b11 = b11 - b22;
                        b11.valuestr = b11.Value_to_Str();
                        b++;  //记录能相减的次数并存到b3相应的位置
                    }
                    char[] ch = b.ToString().ToCharArray();
                    b3.value[b3.value.Length - 1 - a] = ch[ch.Length - 1];
                    for (int i1 = b11.value.Length - 1; i1 > 2; i1--) //每次撤销一个补位
                        b22.value[i1] = b22.value[i1 - 1];
                    b22.value[1] = '0';
                }
                if (((b1.value[0] == '+') && (b2.value[0] == '+')) || ((b1.value[0] == '-') && (b2.value[0] == '-')))  //同号为正
                    b3.value[0] = '+';
                else   //异号为负
                    b3.value[0] = '-';
                b3.valuestr = b3.Value_to_Str();
            }
            return b3;
        }

        public static implicit operator BigInteger(string s)  //实现隐式转换
        {
            BigInteger bi = new BigInteger(s);
            return bi;
        }
    }
}
