using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UnifiedLibraryV1.SystemManagement.Reflection;

namespace UnifiedLibraryV1.IO.Xml{
    public class XmlParser {
        public XDocument XmlDocument { get; private set; }

        protected Dictionary<String, String> ParsedXml { get; private set; }
        protected List<Object> ComplexXml;

        public XmlParser(){
            ParsedXml = new Dictionary<string, string>();
            ComplexXml = new List<Object>();
        }

        public void XDocumentToMap(String root){
           ParsedXml = XmlDocument.Descendants(root).Elements().ToDictionary(e => e.Name.LocalName, e => (string)e);
        }
        
        public void XDocumentToMap(){
            ParsedXml = XmlDocument.Descendants().Elements().ToDictionary(e => e.Name.LocalName, e => (string)e);
        }
        
        public void ReadFromServer(String uri){
            XmlDocument = XDocument.Load(uri);
        }
        
        [Obsolete]
        private Type ExtractTypeFromDocument(){
            return Type.GetType(XmlDocument.Descendants().First().Name.LocalName, true);
        }

        public dynamic ReadComplexDocument(String ComplexXml){
            ReadFromWebRequest(ComplexXml);
            dynamic finalObject = new ExpandoObject();

            foreach (var element in XmlDocument.Elements())
                ReadComplexElement(finalObject, element);

            return finalObject;
        }

        public void ReadComplexElement(dynamic parent, XElement node){
            if (node.HasElements){
                var item = new ExpandoObject();
                if (node.Elements(node.Elements().First().Name.LocalName).Count() > 1){
                    var list = new List<dynamic>();
                    foreach (var element in node.Elements()){
                        ReadComplexElement(list, element);
                    }
                    AddProperty(item, node.Elements().First().Name.LocalName, list);
                    AddProperty(parent, node.Name.ToString(), item);
                }
                else{
                    foreach (var attribute in node.Attributes()){
                        AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
                    }
                    foreach (var element in node.Elements()){
                        ReadComplexElement(item, element);
                    }
                    AddProperty(parent, node.Name.ToString(), item);
                }
            }
            else{
                AddProperty(parent, node.Name.ToString(), node.Value.Trim());
            }


        }

        private void AddProperty(dynamic parent, string name, object value){
            if (parent is List<dynamic>){
                (parent as List<dynamic>).Add(value);
            } else {
                (parent as IDictionary<String, object>)[name] = value;
            }
        }


        public void ReadDocument(String LocalPath){
            XmlDocument = XDocument.Load(LocalPath);
        }

        public void SelectDocument(XDocument document){
            using (var reader = document.CreateReader())
                XmlDocument = XDocument.Load(reader);
        }

        public void ReadFromWebRequest(String Xml){
            XmlDocument = XDocument.Parse(Xml);
        }

        public bool ContainsKey(String key){
            return ParsedXml.ContainsKey(key);
        }

        public bool ContainsValue(String value){
            return ParsedXml.ContainsValue(value);
        }

        public IEnumerable<String> Values(){
            foreach (var value in ParsedXml.Values)
                yield return value;
        }

        public String ConvertToXml(Object obj){
            XmlSerializer xsSubmit = new XmlSerializer(obj.GetType());
            String xml;
            using (StringWriter sww = new StringWriter()){
                using (XmlWriter writer = XmlWriter.Create(sww)){
                    xsSubmit.Serialize(writer, obj);
                    xml = sww.ToString(); 
                }
            }

            return xml;
        }
    }
}
