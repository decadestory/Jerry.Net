using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.Base.Tools
{
    public static class Logger
    {
       static string LogPath { get; set; }

        static Logger(){
            LogPath = AppDomain.CurrentDomain.BaseDirectory;
         }

        public static void Info(string content,bool saveToDb=false)
        {

        }

        public static void Warn(string content, bool saveToDb = false)
        {

        }

        public static void Error(string content, bool saveToDb = false)
        {

        }

        public static void Fatal(string content, bool saveToDb = false)
        {

        }
    }
}
