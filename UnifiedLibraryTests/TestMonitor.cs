using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifiedLibraryV1.IO.Log;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnifiedLibraryTests{
    [TestClass]
    public class TestMonitor{
        [TestMethod]
        public void TestMonitorFactory(){
            Log.UpdatePath(@"C:\Users\Axel\Documents\Visual Studio 2015\OtherTests\Logs\");
            LogMonitorFactory._AutoFlush = false;

            List<LogMonitor> list = new List<LogMonitor>();
            for(var i = 0; i < 5; ++i)
                list.Add(LogMonitorFactory.CreateMonitor());


            Parallel.ForEach(list, 
                obj =>  {
                    obj.Trigger(new Exception("Bonjour moniteur!"),true,true);
                    obj.Trigger("Bonjour moniteur!", LogLevel._INFORMATION, true, false);
                }
            );

            LogMonitorFactory.StopAll();
        }



    }
}
