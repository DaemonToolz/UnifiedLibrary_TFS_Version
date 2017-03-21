using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedLibraryV1.SystemManagement.RegistryManager;

namespace UnifiedLibraryTests
{
    [TestClass]
    public class TestRegistrySecurity
    {
        [TestMethod]
        public void TestBasicSecurity(){
            BasicSecurity Bs = new BasicSecurity(true, 5);
            String generate = Bs.GenerateSecurityCode();

            Assert.IsNotNull(generate);
            Console.WriteLine(generate);
        }
    }
}
