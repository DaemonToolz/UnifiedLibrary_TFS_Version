using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Network.Web{
    public static class SimpleDownloader {
        public static void Download(String RemotePath, String Filename){
            using (var client = new WebClient()){
                client.DownloadFile(RemotePath, Filename);
            }
        }
    }
}
