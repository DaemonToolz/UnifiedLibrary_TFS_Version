using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedLibraryV1.Network.FTP;

namespace UnifiedLibraryTests{
    [TestClass]
    public class TestFtp {
        [TestMethod]
        public void TestDownload(){
           // FTPClient client = new FTPClient("ftp://192.168.1.86:21/Test/", @"C:\Users\Axel\Documents\Visual Studio 2015\OtherTests\", "", "");
           // Console.WriteLine(client.Download("Facture 14-101.xlsx"));
        }

        [TestMethod]
        public void TestUpload(){
           // FTPClient client = new FTPClient("ftp://192.168.1.86:21/Test/", @"C:\Users\Axel\Documents\Visual Studio 2015\OtherTests\", "", "");
           // Assert.IsTrue(client.Upload("StageA4.txt"));
        }

    }
}
