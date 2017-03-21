using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifiedLibraryV1.IO.Log;

namespace UnifiedLibraryTests
{
    /// <summary>
    /// Summary description for TestLogger
    /// </summary>
    [TestClass]
    public class TestLogger{

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestWrite() {
            //Log.UpdatePath(@"C:\Users\Axel\Documents\Visual Studio 2015\OtherTests\");
            //Log.AddToLog("Contenu vide!");
            //Log.AddToLog(new Exception("Exception vide!"));
            //Log.AddToLog(new LogContent(LogLevel._VERBOSE, "Me!", "Void content", DateTime.Now.ToShortTimeString(), new Exception("Void")));
            
        }
    }
}
