using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnifiedLibraryV1.SystemManagement.Infos;

namespace UnifiedLibraryV1.Network.FTP
{
    public abstract class FTP : IFtp {

        public FTP(String Remote, String LocalDestination, String User, String pwd) {
            RemoteFtpPath = Remote;
            LocalDestinationPath = LocalDestination;
            Username = User;
            Password = pwd;

            UploadWorker = new BackgroundWorker();
            UploadWorker.WorkerReportsProgress = true;

            DownloadWorker = new BackgroundWorker();
            DownloadWorker.WorkerReportsProgress = true;
        }

        public String RemoteFtpPath         { get; protected set; }
        public String LocalDestinationPath  { get; protected set; }
        public String Username              { get; protected set; }
        public String Password              { get; protected set; }
        public Int64  Filesize              { get; protected set; }
        public String Filename              { get; set; }
        public BackgroundWorker UploadWorker   { get; protected set; }
        public BackgroundWorker DownloadWorker { get; protected set; }


        public Int32 Progress { get; protected set; }

        #region Abstraction
        public abstract String Download(string filename, bool reportProgress);
        public abstract String Download(string filename, string path, bool reportProgress);
        public abstract String Download(string filename, string path, string remote, bool reportProgress);
        public abstract bool Upload(string filename, bool reportProgress);
        public abstract bool Upload(string filename, string path, bool reportProgress);

        public abstract bool Upload(string filename, string path, string remote, bool reportProgress);

        public abstract Int64 GetLocalFilesize(string filename);
        public abstract Int64 GetRemoteFilesize(string filename, string remote);

        #endregion

        public void OutPath(String path){
            if (path != null && path.Trim().Count() != 0){
                if (path.Equals(LocalDestinationPath)) return;
                LocalDestinationPath = path;
                if (Directory.Exists(path)) LocalDestinationPath = path;
                else
                    try { Directory.CreateDirectory(path); }
                    catch { LocalDestinationPath = SystemFolder.GetPath(KnownFolder.Downloads); }
            }
            else
                LocalDestinationPath = SystemFolder.GetPath(KnownFolder.Downloads);
        }


        public void UplPath(String remote){
            if (remote != null && remote.Trim().Count() != 0)
            {
                if (remote.Equals(RemoteFtpPath)) return;
                RemoteFtpPath = remote;
            }
        }

        protected Boolean FileIsValid(String file){
            return file != null && file.Trim().Count() != 0 && File.Exists(LocalDestinationPath + file);
        }

        protected Boolean RemoteIsValid(){
            return RemoteFtpPath != null && RemoteFtpPath.Trim().Count() != 0;
        }

        protected Boolean LocalIsValid(){
            return LocalDestinationPath != null && LocalDestinationPath.Trim().Count() != 0 && Directory.Exists(LocalDestinationPath);
        }

        public void ChangeCredential(String User, String newPassword){
            Username = User;
            Password = newPassword;
        }


    }
}
