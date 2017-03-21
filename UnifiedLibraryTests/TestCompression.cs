using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedLibraryTests.Dependencies;
using UnifiedLibraryV1.SystemManagement.Compression;
using UnifiedLibraryV1.SystemManagement.Infos;
using UniversalLibraryCS.SystemManagement.Infos;

namespace UnifiedLibraryTests{
    [TestClass]
    public class TestCompression
    {
        [TestMethod]
        public void TestCompressionString()
        {
            DataCompressor<String> dc = new DataCompressor<string>();
            String input = "dsnfo azqngozengoejgaoeigzeisojgozeirgjdo fjzeoig fgo hzgo rfregpa erghez";
            byte[] output;
            Assert.IsNotNull(output = dc.Compress<String>(input));

            foreach (var bit in output)
                Console.Write(bit);

            Console.WriteLine("");

            output = dc.Compress<String>(input = "Et mon cul c'est du téflon?");

            Assert.IsNotNull(input = dc.Decompress<String>(output));

            Console.WriteLine(input);

        }

        [TestMethod]
        public void TestCompressionObjects()
        {
            DataCompressor<Level2Dummy> dc = new DataCompressor<Level2Dummy>();
            Level2Dummy l2d = new Level2Dummy();
            l2d.SignedLongDummy = 5165464654;
            l2d.StringDummy = "4dfs95f4e6544";

            byte[] output;
            Assert.IsNotNull(output = dc.CompressNew(ref l2d));
            foreach (var bit in output) Console.Write(bit);
            Assert.IsNotNull(l2d = dc.DecompressAll().First());

            Console.WriteLine(l2d);

        }


        [TestMethod]
        public void TestCompressionFile() { 

            FileCompressor fp = new FileCompressor();
            fp.UpdateDirectoryPath(Path.Combine(SystemFolder.GetPath(KnownFolder.Downloads) + @"\Tests\DirPath\"));
            DirectoryInfo di = new DirectoryInfo(SystemFolder.GetPath(KnownFolder.Downloads) + @"\Tests\Out\");
            DirectoryInfo rd = new DirectoryInfo(SystemFolder.GetPath(KnownFolder.Downloads) + @"\Tests\Ref\");
            fp.OutFile = null;
            fp.Level = UnifiedLibraryV1.Network.Monitor.MessageLevel.Verbose;

            fp.Verbose = (double dbl, string str)=>{
                Console.WriteLine("{0}% | {1}", dbl, str);
            };

            fp.Simple = (double dbl) => Console.WriteLine("{0}%", dbl);
            fp.Information = (string str) => Console.WriteLine(str);

            Assert.IsNotNull(fp.Level);
            Assert.IsNotNull(fp.Verbose);
            fp.Verbose.Invoke(0, "toto");
            Console.WriteLine(di.ToString());
            Console.WriteLine(di.FullName);
            fp.UpdateOutputDirectory(rd, di, false);

            Console.WriteLine(fp.OutputDirectory);
            fp.Compress(null);
            fp.Compress(new DirectoryInfo(fp.DirectoryPath));
        }

        [TestMethod]
        public void TestDecompressionFile(){
            FileCompressor fp = new FileCompressor();
            fp.UpdateDirectoryPath(Path.Combine(SystemFolder.GetPath(KnownFolder.Downloads) + @"\Tests\DirPath\"));
            DirectoryInfo di = new DirectoryInfo(SystemFolder.GetPath(KnownFolder.Downloads) + @"\Tests\Out\");
            DirectoryInfo rd = new DirectoryInfo(SystemFolder.GetPath(KnownFolder.Downloads) + @"\Tests\Ref\");
            fp.OutFile = null;

            fp.Level = UnifiedLibraryV1.Network.Monitor.MessageLevel.Verbose;

            fp.Verbose = (double dbl, string str) => {
                Console.WriteLine("{0}% | {1}", dbl, str);
            };


            fp.Verbose.Invoke(0, "toto");
            fp.Simple = (double dbl) => Console.WriteLine("{0}%", dbl);
            fp.Information = (string str) => Console.WriteLine(str);

            Console.WriteLine(di.ToString());
            Console.WriteLine(di.FullName);
            fp.UpdateOutputDirectory(rd, di, false);

            Console.WriteLine(fp.OutputDirectory);
            //fp.Decompress();

        }

    }

}
