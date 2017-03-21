using System;

namespace UnifiedLibraryV1.Network.FTP
{
    interface IFtp {
        
        String Download(String filename, bool reportProgress);
        String Download(String filename, String path, bool reportProgress);
        String Download(String filename, String path, String remote, bool reportProgress);



        Boolean Upload(String filename, bool reportProgress);
        Boolean Upload(String filename, String path, bool reportProgress);
        Boolean Upload(String filename, String path, String remote, bool reportProgress);


        void OutPath(String path);
        void UplPath(String remote);
    }
}
