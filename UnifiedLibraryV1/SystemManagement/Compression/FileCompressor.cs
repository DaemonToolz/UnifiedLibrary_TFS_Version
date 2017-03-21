using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedLibraryV1.Network.Monitor;

namespace UnifiedLibraryV1.SystemManagement.Compression{
    public class FileCompressor : Monitor{
        public String DirectoryPath { get; set; }
        //public String FilePath { get; set; }
        public String OutFile { get; set; }

        public List<FileInfo> Cache { get; private set; }
        protected Dictionary<FileInfo, byte[]> CacheContent;

        #region Short Memory Region
        private FileInfo      LastFileInfo;
        private DirectoryInfo LastDirectoryInfo;
        #endregion

        #region cache
        protected static readonly Int32 _MAXIMUM_CACHE_SIZE = 128;
        protected static readonly Int32 _MAXIMUM_CONTENT_SIZE = 4096;
        private Int32 CyclicDeletionIndex = 0;

        private bool activeReferenceDirectory = false, outputPathIsReference = false;
        private bool _ShadowRefDirectory, _ShadowOutDirectory;

        public FileCompressor(){
            Cache = new List<FileInfo>();
            CacheContent = new Dictionary<FileInfo, byte[]>();
            UseOutPut = true;
            //Level = MessageLevel.None;
        }

        // Keeping the last active state used
        private bool ActiveReferenceDirectory{
            get { return activeReferenceDirectory; }
            set {
                _ShadowRefDirectory = activeReferenceDirectory;
                activeReferenceDirectory = value;
            }
        }

        private bool OutputPathIsReference{
            get { return outputPathIsReference; }
            set {
                _ShadowOutDirectory = outputPathIsReference;
                outputPathIsReference = value;
            }
        }

        public FileInfo         CompressedFiles     { get; private set; }
        public DirectoryInfo    ReferenceDirectory  { get; private set; }
        public DirectoryInfo    OutputDirectory     { get; private set; }
        #endregion

        public void DisableReferenceDirectory(){
            ActiveReferenceDirectory = false;
            OutputPathIsReference = false;
        }

 
        public void EnableReferenceDirectory(){
            ActiveReferenceDirectory = _ShadowRefDirectory;
            OutputPathIsReference    = _ShadowOutDirectory;
        }

        public void UpdateReferenceDirectory(DirectoryInfo dir){
            if (dir == null || !dir.Exists) throw new IOException("Reference directory invalid");

            ActiveReferenceDirectory = true;
            OutputPathIsReference = false;
            ReferenceDirectory = dir;
        }

        public void UpdateOutputDirectory(DirectoryInfo referenceDir, DirectoryInfo outputDir = null, bool refAsOutput = false){
            if (outputDir == null || !outputDir.Exists) refAsOutput = true;

            if (referenceDir != null && referenceDir.Exists){
                if (!refAsOutput){
                    OutputDirectory = outputDir;
                    UpdateReferenceDirectory(referenceDir);
                } else {
                    OutputDirectory = ReferenceDirectory = outputDir;
                    OutputPathIsReference = true;
                    ActiveReferenceDirectory = true;
                }
            }
            else
                throw new IOException("At least an active directory must be defined.");
        }

        private void Copy(String source, String target) {
            if(!Directory.Exists(target))
                Directory.CreateDirectory(target);

            foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                if (!Directory.Exists(dirPath.Replace(source, target)))
                    Directory.CreateDirectory(dirPath.Replace(source, target));
            
            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(source, target), true);
        }

        private void Copy(DirectoryInfo source, DirectoryInfo target){
            int index = 1;

            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles()){
                Display(((index++*100 / source.GetFiles().Count())), "Copying file " + source.Name + " > " + fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            index = 1;
            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories()){
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                Display(((index++*100 / source.GetFiles().Count())), "Copying Directory " + source.Name + " > " + diSourceSubDir.Name);

                Copy(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void Copy(DirectoryInfo target, params FileInfo[] source){
            if (source == null) throw new IOException("Sources cannot be null");
            if (target == null) throw new IOException("Target directory cannot be null");
            if (!target.Exists)
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source)
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);

        }
   
        public void UpdateDirectoryPath(String path){
            if (path == null) throw new IOException("The path must be valid");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            DirectoryPath = path;
        }

        public void Compress(DirectoryInfo directorySelected) {
            if (!ActiveReferenceDirectory && directorySelected == null) throw new IOException("The selected directory must be valid");
            int index = 1;

            List<FileInfo> currentFiles = null;

            if (ActiveReferenceDirectory && directorySelected != null) {
                FileInfo tempo;

                currentFiles = ReferenceDirectory.GetFiles().ToList();

                foreach (var file in directorySelected.GetFiles()) {
    
                    tempo = new FileInfo(Path.Combine(ReferenceDirectory.FullName, file.Name));
                    Display((index++ * 100 / directorySelected.GetFiles().Count()), "Hidding " + file.Name);
                   
                    if (tempo.Attributes == FileAttributes.Hidden) continue;
                    if (File.Exists(tempo.FullName)){
                        currentFiles.Remove(currentFiles.Find(cf => cf.FullName.Equals(tempo.FullName)));
                        tempo.Delete();
                    }
                }

                currentFiles.ForEach(file => File.SetAttributes(file.FullName, File.GetAttributes(file.FullName) | FileAttributes.Hidden));
                Copy(directorySelected, ReferenceDirectory);

            }

            index = 1;

            LastDirectoryInfo = ActiveReferenceDirectory ? ReferenceDirectory : directorySelected;
            foreach (FileInfo fileToCompress in LastDirectoryInfo.GetFiles()) {

                Display((index++*100 / LastDirectoryInfo.GetFiles().Count()), "Zipping " + fileToCompress.Name);
                if (Cache.Count() > _MAXIMUM_CACHE_SIZE)
                    Cache.Clear();
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ArchiveExtensions.GZIP.Name)
                    {
                        using (FileStream compressedFileStream = File.Create(Path.Combine(OutputDirectory.FullName, (OutFile != null ? OutFile : fileToCompress.Name) + ArchiveExtensions.GZIP.Name)))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }

                        LastFileInfo = new FileInfo(Path.Combine((OutputDirectory != null) ? OutputDirectory.FullName : DirectoryPath, fileToCompress.Name + ArchiveExtensions.GZIP.Name));
                    }
                }

            }

            index = 1;
            if (ActiveReferenceDirectory && directorySelected != null)
                currentFiles.ForEach(file => {
                    Display((index++ * 100 / currentFiles.Count()), "Checking hidden file " + file.Name);
                    File.SetAttributes(file.FullName, file.Attributes & ~FileAttributes.Hidden);
                    }
                );
        }


        public void Decompress(FileInfo fileToDecompress) {
            if (fileToDecompress == null || !fileToDecompress.Exists) throw new IOException("File to decompress must exist.");
            LastFileInfo = fileToDecompress;

            if (Cache.Count() > _MAXIMUM_CACHE_SIZE)
                Cache.Clear();

            Cache.Add(fileToDecompress);



            using (FileStream originalFileStream = fileToDecompress.OpenRead()) {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(Path.Combine(OutputDirectory.FullName, newFileName))) {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress)) {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }

        public void CompressFile(string sDir, string sRelativePath, GZipStream zipStream) {
            //Compress file name
            char[] chars = sRelativePath.ToCharArray();
            zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
            foreach (char c in chars)
                zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));

            //Compress file content
            byte[] bytes = File.ReadAllBytes(Path.Combine(sDir, sRelativePath));
            zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
            zipStream.Write(bytes, 0, bytes.Length);
        }

        public bool DecompressFile(string sDir, GZipStream zipStream, ProgressInformationDelegate progress) {
            //Decompress file name
            byte[] bytes = new byte[sizeof(int)];
            int Readed = zipStream.Read(bytes, 0, sizeof(int));
            if (Readed < sizeof(int))
                return false;

            int iNameLen = BitConverter.ToInt32(bytes, 0);
            bytes = new byte[sizeof(char)];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iNameLen; i++)
            {
                zipStream.Read(bytes, 0, sizeof(char));
                char c = BitConverter.ToChar(bytes, 0);
                sb.Append(c);
            }
            string sFileName = sb.ToString();
            if (progress != null)
                progress(sFileName);

            //Decompress file content
            bytes = new byte[sizeof(int)];
            zipStream.Read(bytes, 0, sizeof(int));
            int iFileLen = BitConverter.ToInt32(bytes, 0);

            bytes = new byte[iFileLen];
            zipStream.Read(bytes, 0, bytes.Length);

            string sFilePath = Path.Combine(sDir, sFileName);
            string sFinalDir = Path.GetDirectoryName(sFilePath);
            if (!Directory.Exists(sFinalDir))
                Directory.CreateDirectory(sFinalDir);

            using (FileStream outFile = new FileStream(sFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                outFile.Write(bytes, 0, iFileLen);

            return true;
        }

        public void CompressDirectory(string sInDir, string sOutFile, ProgressInformationDelegate progress){
            string[] sFiles = Directory.GetFiles(sInDir, "*.*", SearchOption.AllDirectories);
            int iDirLen = sInDir[sInDir.Length - 1] == Path.DirectorySeparatorChar ? sInDir.Length : sInDir.Length + 1;

            using (FileStream outFile = new FileStream(sOutFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
                foreach (string sFilePath in sFiles)
                {
                    string sRelativePath = sFilePath.Substring(iDirLen);
                    if (progress != null)
                        progress(sRelativePath);
                    CompressFile(sInDir, sRelativePath, str);
                }
        }

        public void DecompressToDirectory(string sCompressedFile, string sDir, ProgressInformationDelegate progress) {
            using (FileStream inFile = new FileStream(sCompressedFile, FileMode.Open, FileAccess.Read, FileShare.None))
            using (GZipStream zipStream = new GZipStream(inFile, CompressionMode.Decompress, true))
                while (DecompressFile(sDir, zipStream, progress)) ;
        }


        public void DeleteCachedFiles(bool Permanent){
            byte[] buffer;

            int index = 1;

            Cache.ForEach(fileInCache => {
                if (!Permanent){

                    if (CacheContent.Count > _MAXIMUM_CONTENT_SIZE)
                        CacheContent.Remove(CacheContent.ElementAt(0).Key);

                    Display((index++ * 100 / Cache.Count), "Deleting from cache "+ fileInCache.Name);

                    using (var stream = fileInCache.OpenRead()){
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, (int)stream.Length);
                        CacheContent.Add(fileInCache, buffer);
                    }
                } 
                fileInCache.Delete();
            });

            if (Permanent){
                Cache.Clear();
            }
        }

        public void Backup(int endIndex, int startingIndex = 0){
            if (endIndex > CacheContent.Count - 1 || endIndex < startingIndex)
                endIndex = CacheContent.Count - 1;

            int index = 1;

            foreach (var fileAndContent in CacheContent){

                if (fileAndContent.Key.Exists) continue;
                try{
                    fileAndContent.Key.Create();
                    using (var stream = fileAndContent.Key.OpenWrite()) 
                        stream.Write(fileAndContent.Value, 0, fileAndContent.Value.Count());
                }
                catch{
                    // Error while writting
                }

                Display((index++ * 100 / CacheContent.Count), "Backup of " + fileAndContent.Key.Name);
            }

            CacheContent.Clear();

        }
    }
}

