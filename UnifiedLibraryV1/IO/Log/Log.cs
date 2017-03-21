using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Log{
    public class Log {
        public static String DefaultPath { get; protected set; }
        public static String DefaultDate { get; protected set; }
        public static String FileName { get; protected set; }
        public static String FileExtension { get; protected set; }

        public static String LastContentRead { get; protected set; }

        private static readonly Mutex SharedMutex;

        static Log(){
     
            SharedMutex = new Mutex();

            SharedMutex.WaitOne();
            DefaultPath = "./Logs/";
            FileExtension = ".log";

            if (!Directory.Exists(DefaultPath))
                Directory.CreateDirectory(DefaultPath);

            DefaultDate = DateTime.Now.ToString("ddMMyyyy");
            FileName = "AppLog_" + DefaultDate + FileExtension;
           
            SharedMutex.ReleaseMutex();
            Prepare();
        }

        public static void Write(String path, String filename, String Content) {
            if (Directory.Exists(path) && File.Exists(path + filename)){
                SharedMutex.WaitOne();
                File.AppendAllText(path + filename, Content);
                SharedMutex.ReleaseMutex();
            }
        }

        public static String Read(String path, String filename){
            SharedMutex.WaitOne();
            if (!Directory.Exists(path) || !File.Exists(path + filename))
                LastContentRead = String.Empty;
            else
                LastContentRead = File.ReadAllText(path + filename);
            SharedMutex.ReleaseMutex();
            return LastContentRead;
        }

        public static void Prepare(){

            SharedMutex.WaitOne();
            UpdateDate();

            if (!Directory.Exists(DefaultPath))
                Directory.CreateDirectory(DefaultPath);

            if (!File.Exists(DefaultPath + FileName))
                File.Create(DefaultPath + FileName).Close(); ;

            SharedMutex.ReleaseMutex();
        }

        public static void UpdatePath(String path) {
            SharedMutex.WaitOne();
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                DefaultPath = path;
            }
            catch{
                SharedMutex.ReleaseMutex();
                throw;
            }
            SharedMutex.ReleaseMutex();
           
        }

        public static void UpdateDate(){
            {
                String temp;
                if (!DefaultDate.Equals(temp = DateTime.Now.ToString("ddMMyyyy")))
                    DefaultDate = temp;
            }
        }

        public static void AddToLog(LogContent lc) {
            Prepare();
            SharedMutex.WaitOne();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(DefaultPath + FileName, true))
                    file.WriteLine(lc.ToString());
            SharedMutex.ReleaseMutex();
        }

        public static void AddToLog(params LogContent[] lc){
            Prepare();
            SharedMutex.WaitOne();
                lc.OrderBy(obj => obj.Author).ToList().ForEach(obj => File.AppendAllText(DefaultPath + FileName, obj.ToString()));
            SharedMutex.ReleaseMutex();
        }

        public static void AddToLog(List<LogContent> lc){
            Prepare();
            SharedMutex.WaitOne();
            lc.OrderBy(obj => obj.Author).ToList().ForEach(obj => File.AppendAllText(DefaultPath + FileName, obj.ToString()));
            SharedMutex.ReleaseMutex();
        }

        public static void AddToLog(String content){
            Prepare();
            SharedMutex.WaitOne();
                File.AppendAllText(DefaultPath + FileName, content + System.Environment.NewLine);
            SharedMutex.ReleaseMutex();
        }

        public static void AddToLog(params String[] contents) {
            Prepare();
            SharedMutex.WaitOne();
                File.AppendAllLines(DefaultPath + FileName, contents);
                File.AppendAllText(DefaultPath + FileName, System.Environment.NewLine);
            SharedMutex.ReleaseMutex();
        }

        public static void AddToLog(Object obj){
            Prepare();
            SharedMutex.WaitOne();
                File.AppendAllText(DefaultPath + FileName, obj.ToString() + System.Environment.NewLine);
            SharedMutex.ReleaseMutex();
        }

        public static void AddToLog(params Object[] contents){
            Prepare();
            SharedMutex.WaitOne();
                contents.ToList().ForEach(obj => File.AppendAllText(DefaultPath + FileName, obj.ToString()));
                File.AppendAllText(DefaultPath + FileName, System.Environment.NewLine);
            SharedMutex.ReleaseMutex();
        }



        public static String ExtractAllFromLog(){
           

            return null;
        }

        public static Exception ExtractExceptionFromLog(){
            

            return null;
        }
    }
}
