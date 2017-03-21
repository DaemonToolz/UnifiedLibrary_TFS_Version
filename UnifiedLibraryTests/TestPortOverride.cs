using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifiedLibraryV1.Network.Monitor;
using System.Security.Principal;

namespace UnifiedLibraryTests{
    [TestClass]
    public class TestPortOverride{
        public static bool IsAdministrator(){
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                    .IsInRole(WindowsBuiltInRole.Administrator);
        }

        [TestMethod]
        public void TestBusys(){
            Assert.IsNotNull(PortOverriding.BusyPorts());
        }

        [TestMethod]
        public void TestOverrideOne(){
            if (IsAdministrator()){
                PortOverriding.BlockPort(20000);
                PortOverriding.FreeAll();
            }
        }

    }
}
