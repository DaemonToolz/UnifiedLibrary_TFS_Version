using Renci.SshNet;
using System;
using System.IO;

namespace UnifiedLibraryV1.Network.FTP
{
    class SFTPClient : FTP {
        public SFTPClient(String Remote, String LocalDestination, String User, String pwd) : base(Remote, LocalDestination, User, pwd){
        }

        public String RemoteHost { get; protected set; }
        public Int32  Port { get; protected set; }

        public override String Download(String filename, bool reportProgress = false)
        {
            try {
                if (!FileIsValid(filename))
                    throw new IOException("The current filename is either null or empty.");

                if (!RemoteIsValid())
                    throw new Exception("Remote directory is not set.");

                if (!LocalIsValid())
                    throw new Exception("Local direction is not set.");

                using (var sftp = new SftpClient(RemoteHost, Port, Username, Password)) {
                    sftp.Connect();

                    using (var file = File.OpenWrite(LocalDestinationPath + filename)) {
                        sftp.DownloadFile(filename, file);
                    }

                    sftp.Disconnect();
                }

                return "Download completed";
            }
            catch (Exception e)
            {
                return "Error while download : " + (e.ToString());
            }
        }
        public override string Download(string filename, string path, bool reportProgress = false)
        {
            OutPath(path);
            return Download(filename);
        }

        public override String Download(String filename, String path, String remote, bool reportProgress = false)
        {
            UplPath(remote);
            return Download(filename, path);
        }

        public override Boolean Upload(String filename, bool reportProgress = false)
        {
            try{
                using (var client = new SftpClient(RemoteHost, Port, Username, Password))
                {
                    client.Connect();

                    client.ChangeDirectory(RemoteFtpPath);

                    using (var fileStream = new FileStream(filename, FileMode.Open))
                    {
                        client.BufferSize = 4 * 1024; // bypass Payload error large files
                        client.UploadFile(fileStream, Path.GetFileName(filename));
                    }
                }
                return true;
            }
            catch{
                return false;
            }
        }

        public override bool Upload(string filename, string path, bool reportProgress = false)
        {
            OutPath(path);
            return Upload(filename);
        }

        public override bool Upload(string filename, string path, string remote, bool reportProgress = false)
        {
            UplPath(remote);
            return Upload(filename, path);
        }

        public override long GetLocalFilesize(string filename)
        {
            throw new NotImplementedException();
        }

        public override long GetRemoteFilesize(string filename, string remote)
        {
            throw new NotImplementedException();
        }
    }
}
