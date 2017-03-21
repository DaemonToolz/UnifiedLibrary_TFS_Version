using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifiedLibraryV1.IO.Xml;
using UnifiedLibraryTests.Dependencies;

namespace UnifiedLibraryTests{
    [TestClass]
    public class TestXmlParser{
        [TestMethod]
        public void TestParsing(){
            XmlParser parser = new XmlParser();

            String Xml = @"<root>
                                <newfile>FileA</newfile>
                                <document>DocumentA</document>
                           </root>";

            parser.ReadFromWebRequest(Xml);
            parser.XDocumentToMap();

            Assert.IsNotNull(parser.Values());

            foreach (var v in parser.Values())
                Console.WriteLine(v);
        }

        [TestMethod]
        public void TestSerialization(){
            XmlParser parser = new XmlParser();

            //String xmlOutput;
            dynamic dynamicObject0 = new Level2Dummy();
            dynamicObject0.SignedLongDummy = 0xFFFFFF;
            dynamicObject0.StringDummy = "Dummy";

            Assert.IsNotNull(/*xmlOutput = */parser.ConvertToXml(dynamicObject0));

            Console.WriteLine((parser.ConvertToXml(dynamicObject0)));

            ParsingDummy testObject = new ParsingDummy();
            testObject.UnsignedIntDummy = 0;
            testObject.SublevelDummy = dynamicObject0;
            /*
            Assert.IsNotNull(xmlOutput = parser.ConvertToXml(testObject));
            Console.WriteLine(parser.ConvertToXml(testObject));


            Assert.IsNotNull(testObject = parser.ReadComplexDocument(xmlOutput));
            Console.WriteLine((testObject).ToString());
            */
        }

    }
}
