using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifiedLibraryV1.IO.Json;
using UnifiedLibraryTests.Dependencies;
using UnifiedLibraryV1.IO.Xml;
using System.Xml.Linq;
using System.Dynamic;

namespace UnifiedLibraryTests{
    [TestClass]
    public class TestJson{
        [TestMethod]
        public void TestParseToJson(){
            Level2Dummy dummy = new Level2Dummy();
            dummy.StringDummy = "toto";
            dummy.SignedLongDummy = 0x0215466;

            String JsonString;
            JsonParser<Level2Dummy> jp = new JsonParser<Level2Dummy>();
            Assert.IsNotNull(JsonString = jp.Serialize(dummy));

            Console.WriteLine(JsonString);

            dummy = null;
            Assert.IsNotNull(dummy = jp.Deserialize(JsonString));

        }

        [TestMethod]
        public void TestParseXml(){
            String Xml = @"<root>
                                <newfile>FileA</newfile>
                                <document>DocumentA</document>
                           </root>";

            XmlParser xmlParser = new XmlParser();
            xmlParser.ReadFromWebRequest(Xml);
            xmlParser.XDocumentToMap();

            Xml = "";

            JsonParser<XDocument> jp = new JsonParser<XDocument>();
            Assert.IsNotNull(Xml = jp.XToJson(xmlParser.XmlDocument));
            Console.WriteLine(Xml);
            Assert.IsNotNull(jp.JsonToX(Xml));

            xmlParser.SelectDocument(jp.JsonToX(Xml));
            Assert.IsNotNull(xmlParser.XmlDocument);
        }


        [TestMethod]
        public void TestParseDynamic() {
            dynamic myObject = new ExpandoObject();
            myObject.Value = "05s689ds+a0";
            String json;

            JsonParser<dynamic> jp = new JsonParser<dynamic>();
            Assert.IsNotNull(json = jp.SerializeDynamic(myObject));

            myObject = null;
            Console.WriteLine(json);

            Assert.IsNotNull(myObject = jp.DeserializeDynamic(json));
            Console.WriteLine(myObject.Value);

        }

    }
}
