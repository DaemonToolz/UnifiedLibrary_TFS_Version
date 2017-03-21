using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.SystemManagement.Compression{
    public class DataCompressor<T> : Monitor {
        public List<T> Objects{ get; private set; }
        private byte[] Compressed;
        private List<int> ObjectSizes;

        public DataCompressor(){
            Objects = new List<T>();
            ObjectSizes = new List<int>();
        }

        public byte[] CompressNew(ref T obj) {
            Objects.Clear();
            Objects.Add(obj);
            ObjectSizes.Clear();
            using (MemoryStream ms = new MemoryStream()){
                ms.Seek(0, SeekOrigin.Begin);
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true)){
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(zs, obj);
                }
                Compressed = ms.ToArray();
                ObjectSizes.Add(Compressed.Count());
            }
            return Compressed;
        }

        public T DecompressNew(ref byte[] data){
            Compressed = data;
            Objects.Clear();
            using (MemoryStream ms = new MemoryStream(Compressed)){
                ms.Seek(0, SeekOrigin.Begin);
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress, true))
                {
                    Objects.Add((T)(new BinaryFormatter().Deserialize(zs)));
                }
            }
            return Objects.Last();
        }

        public byte[] CompressAll(){
            using (MemoryStream ms = new MemoryStream()){
                ms.Seek(0, SeekOrigin.Begin);

                BinaryFormatter bf = new BinaryFormatter();
                // Première rotation, on récupère la taille de tout les objets en cache
                foreach (var @object in Objects){
                    using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true)){
                        bf.Serialize(zs, @object);
                        ObjectSizes.Add(ms.ToArray().Count());
                    }
                }
            }

            using (MemoryStream ms = new MemoryStream()){
                ms.Seek(0, SeekOrigin.Begin);
                BinaryFormatter bf = new BinaryFormatter();
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true)){
                    foreach (var @object in Objects)
                        bf.Serialize(zs, @object);
                }
                Compressed = ms.ToArray();
            }
            return Compressed;
        }

        public byte[] CompressList(){

            using (MemoryStream ms = new MemoryStream()){
                ms.Seek(0, SeekOrigin.Begin);
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    // Première rotation, on récupère la taille de tout les objets en cache
                    bf.Serialize(zs, Objects);
                    
                }
                Compressed = ms.ToArray();
            }
            return Compressed;
        }

        public List<T> DecompressList(){
            Objects.Clear();
            using (MemoryStream ms = new MemoryStream(Compressed)){
                using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress, true)){
                    Objects = ((List<T>)(new BinaryFormatter().Deserialize(zs)));
                }
            }
            return Objects;
        }

        private byte[] ByteSubArray(byte[] data, int index, int length){
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public List<T> DecompressAll(){
            Objects.Clear();
            BinaryFormatter bf = new BinaryFormatter();
            int minIndex = 0;
            byte[] subArray;

            foreach (var size in ObjectSizes){
                subArray = ByteSubArray(Compressed, minIndex, minIndex + size);
                using (MemoryStream ms = new MemoryStream(subArray)){
                    ms.Seek(0, SeekOrigin.Begin);
                    using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress, true))
                    {
                        Objects.Add((T)(new BinaryFormatter().Deserialize(zs)));
                    }
                }
                minIndex += size;                
            }
            return Objects;
        }

        public void AddToExistingObjects(params T[] objects){
            if (objects == null) return;
            Objects.AddRange(objects);
            ObjectSizes.Clear();
        }

        public void UpdateBytes(params byte[][] newBytes){
            if (newBytes == null)
                return;

            ObjectSizes.Clear();
            Objects.Clear();

            int compressionSize = 0;

            unsafe{
                foreach (var byteArr in newBytes){
                    compressionSize += (/*sizeof(byte)**/byteArr.Count());
                    ObjectSizes.Add(byteArr.Count());
                }
            }

            Compressed = new byte[compressionSize];
            compressionSize = 0;
            foreach (var byteArr in newBytes){
                byteArr.CopyTo(Compressed, (compressionSize));
                compressionSize += byteArr.Count();
            }
        }

        private void CopyTo(Stream src, Stream dest){
            byte[] bytes = new byte[4096];
            int cnt;
            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
                dest.Write(bytes, 0, cnt);
            
        }

        public byte[] Compress<String>(string str){
            var bytes = Encoding.UTF8.GetBytes(str);
            byte[] CompressedStr;
            using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream()){
                    using (var gs = new GZipStream(mso, CompressionMode.Compress, true))
                        CopyTo(msi, gs);
                    CompressedStr= mso.ToArray();
                }

            return CompressedStr;
        }

        public string Decompress<String>(byte[] bytes) { 
            string finalStr;
            using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream()){
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress, true))
                        CopyTo(gs, mso);
                finalStr = Encoding.UTF8.GetString(mso.ToArray());
            }
            return finalStr;
        }
    }
}
