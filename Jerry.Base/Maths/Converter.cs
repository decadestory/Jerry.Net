using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Maths
{
    public static class Converter
    {
        public static decimal ToDecimal(this object val,int len=2)
        {
            var input = Convert.ToDecimal(val).ToString("f" + len);
            return Convert.ToDecimal(input);
             
            Debug.WriteLine(123.00);

        }
    }
}
