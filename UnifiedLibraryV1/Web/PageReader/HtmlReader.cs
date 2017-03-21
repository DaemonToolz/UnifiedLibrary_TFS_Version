using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnifiedLibraryV1.Exceptions.Web;

namespace UnifiedLibraryV1.Web.PageReader{
    public class HtmlReader{
        private static readonly Int32 BUFFER_SIZE = 1024;

        public HtmlWeb Web { get; private set; }
        public HtmlDocument Document { get; private set; }

        public String Html { get; protected set; }
        public List<HtmlNode> Styles { get; private set; }
        public List<HtmlNode> Javascripts { get; private set; }



        public HtmlReader(){
            Web = new HtmlWeb();
        }

        public HtmlDocument ExtractHtml(String Url){
            try{
                return (Document = Web.Load(Url));
            } catch(Exception e){
                throw new HtmlInvalidLinkException("Targetted Url could not be reached", e);
            }
        }

        [Obsolete]
        public String ExtractHtmlToString(String Url) {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK){
                using (Stream receiveStream = response.GetResponseStream()){
                    StreamReader readStream = null;

                    if (response.CharacterSet == null){
                        readStream = new StreamReader(receiveStream);
                    }
                    else{
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    Html = readStream.ReadToEnd();
                    response.Close();
                }

                return Html;
            }

            throw new HtmlInvalidLinkException("Targetted Url could not be reached");
        }

        public void ExtractResources(){
            if (Document == null || !Document.DocumentNode.HasChildNodes ) throw new HtmlEmptyContent("The provided HTML is empty");
            Styles      = Document.DocumentNode.Descendants("link").ToList();
            Javascripts = Document.DocumentNode.Descendants("script").ToList();
        }
    }
}
