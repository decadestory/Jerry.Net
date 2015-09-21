using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jerry.Base.Common;
using Jerry.Base.Common.Exts;
using System.Collections.Generic;
using System.Threading;
using Jerry.Base.Config;
using Jerry.Base.Tools;


namespace Jerry.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var res = new ResultMsg<PageMsg<string>>();
            //  //var er = new ResultMsg<List<TreeMsg>>();

            //  var apps = Configs.GetAppSetting("key1");
            //  var cons = Configs.GetConnSetting("conn");


            for (var i = 0; i < 10; i++) { 
                Logger.Info("这是一个致命错误"+i);
            }



            Assert.IsTrue(1 == 1);

        }
    }
}
