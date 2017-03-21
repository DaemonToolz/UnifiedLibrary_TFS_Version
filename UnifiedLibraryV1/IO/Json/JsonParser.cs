using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UnifiedLibraryV1.IO.Json{
    public class JsonParser<T>{
        public String LastJson { get; protected set; }
        public T      Object { get; protected set; }

        public String XmlToJson<Xml>(Xml document) where Xml : XmlDocument{
            return JsonConvert.SerializeXmlNode(document);
        }

        public String XToJson<Xml>(Xml document) where Xml : XDocument{
            return JsonConvert.SerializeXNode(document);
        }

        public XmlDocument JsonToXml(String content){
            return JsonConvert.DeserializeXmlNode(content);
        }

        public XDocument JsonToX(String content){
            return JsonConvert.DeserializeXNode(content);
        }


        public String Serialize(){
            return LastJson = JsonConvert.SerializeObject(Object);
        }

        public String Serialize(T newObject){
            Object = newObject;
            return Serialize();
        }

        public T Deserialize(){
            return Object = JsonConvert.DeserializeObject<T>(LastJson);
        }

        public T Deserialize(String json){
            LastJson = json;
            return Deserialize();
        }

        #region Special Componants
        public dynamic JsonParseLinq(String content){
            return JObject.Parse(content);
        }

        public dynamic DeserializeDynamic(String content){
            return JsonConvert.DeserializeObject(content);
        }

        public String SerializeDynamic(dynamic dynamicObject){
            return JsonConvert.SerializeObject(dynamicObject);
        }
        #endregion

        
    }
}
