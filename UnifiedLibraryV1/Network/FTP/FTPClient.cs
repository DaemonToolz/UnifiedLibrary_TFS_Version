using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace UnifiedLibraryV1.Network.FTP
{
    public class FTPClient : FTP {
        Boolean UseBinary = true; 
        Boolean UsePassive = false;

        public FTPClient(String Remote, String LocalDestination, String User, String pwd) : base(Remote, LocalDestination, User, pwd) {
            UploadWorker.DoWork   += UploadWorker_DoWork;
            DownloadWorker.DoWork += DownloadWorker_DoWork;
        }

        private void UploadWorker_DoWork(object sender, DoWorkEventArgs e){
            Upload(Filename, true);
        }

        private void DownloadWorker_DoWork(object sender, DoWorkEventArgs e){
            Download(Filename, true);
        }

        public override string Download(string filename, bool reportProgress = false) {
            Progress = 0;
            if (filename == null || filename.Trim().Equals(""))
                    throw new IOException("Invalid file");

            Filename = filename;

                if (!RemoteIsValid())
                    throw new Exception("Remote directory is not set.");

                if (!LocalIsValid())
                    throw new Exception("Local directory is not set.");
                
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemoteFtpPath + Filename);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.KeepAlive = true;
                request.UsePassive = UsePassive;
                request.UseBinary = UseBinary;

                request.Credentials = new NetworkCredential(Username, Password);
            
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {

                        using (FileStream writer = new FileStream(LocalDestinationPath + Filename, FileMode.Create))
                        {
                            var totalRead = 0.0;

                            long length = response.ContentLength;
                            int bufferSize = 2048;
                            int readCount;
                            byte[] buffer = new byte[2048];
                            
                            readCount = responseStream.Read(buffer, 0, bufferSize);
                            while (readCount > 0){
                                writer.Write(buffer, 0, readCount);
                                totalRead += readCount;
                               
                                readCount = responseStream.Read(buffer, 0, bufferSize);

                                if(reportProgress)
                                    DownloadWorker.ReportProgress(Progress = ((int)(totalRead * 100.0 / responseStream.Length)));
                            }
                        }
                    }
                }
                return "Download completed";
            
        }

        // Utilisation si répertoire changeant
        public override string Download(string filename, string path, bool reportProgress = false) {
            OutPath(path);
            return Download(filename, reportProgress);
        }

        public override String Download(String filename, String path, String remote, bool reportProgress = false) {
            UplPath(remote);
            return Download(filename, path, reportProgress);
        }

        // In bytes
        public override long GetLocalFilesize(string filename){
            if (!FileIsValid(filename))
                throw new IOException("The current filename is either null or empty.");

            return new FileInfo(LocalDestinationPath + filename).Length;
        }

        public override long GetRemoteFilesize(string filename, string remote){
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(remote + filename));
            request.Proxy = null;
            request.Credentials = new NetworkCredential(Username, Password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            long size = response.ContentLength;
            response.Close();

            return size;
        }

        public override bool Upload(string filename, bool reportProgress = false) {
            try {
                if (!FileIsValid(filename))
                    throw new IOException("The current filename is either null or empty.");

                Filename = filename;
                if (!RemoteIsValid())
                    throw new Exception("Remote directory is not set.");

                if (!LocalIsValid())
                    throw new Exception("Local directory is not set.");

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(RemoteFtpPath + Filename);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.KeepAlive = true;
                request.UsePassive = UsePassive;
                request.UseBinary = UseBinary;

                request.Credentials = new NetworkCredential(Username, Password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                FileInfo fi = new FileInfo(LocalDestinationPath + Filename);
                request.ContentLength = fi.Length;
                byte[] buffer = new byte[4097];
                int bytes = 0;
                int total_bytes = (int)fi.Length;
                var total = 0.0;

                using (FileStream fs = fi.OpenRead())
                {
                    using (Stream rs = request.GetRequestStream())
                    {
                        while (total_bytes > 0)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            rs.Write(buffer, 0, bytes);
                            total_bytes = total_bytes - bytes;
                            total += total_bytes;

                            if (reportProgress)
                                UploadWorker.ReportProgress(Progress = ((int)(total * 100.0 / rs.Length)));
                        }
                    }
                }
                //fs.Flush();
                FtpWebResponse uploadResponse = (FtpWebResponse)request.GetResponse();
                uploadResponse.Close();
                return true;
            }
            catch {
                return false;
            }
        }

        public override bool Upload(string filename, string path, bool reportProgress = false) {
            OutPath(path);
            return Upload(filename, reportProgress);
        }

        public override bool Upload(string filename, string path, string remote, bool reportProgress = false) {
            UplPath(remote);
            return Upload(filename, path, reportProgress);
        }
        
    }
}
