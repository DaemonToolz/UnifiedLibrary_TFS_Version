using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Log{
    public sealed class LogLevel{

        public readonly String Name;
        public readonly int    Value;

        public static readonly LogLevel _VERBOSE = new LogLevel(1, "Verbose");
        public static readonly LogLevel _INFORMATION = new LogLevel(2, "Information");
        public static readonly LogLevel _DEBUG = new LogLevel(3, "Debug");
        public static readonly LogLevel _ERROR = new LogLevel(4, "Error");

        private LogLevel(int value, String name){
            this.Name = name;
            this.Value = value;
        }

        public override String ToString(){
            return Name;
        }
    }

    public class LogContent : EventArgs{
        public String Author { get; set; }
        public String Content { get; set; }
        public String Date { get; set; }
        public Exception Exception { get; set; }
        public LogLevel Level { get; set; }

        public LogContent(LogLevel level, String aut, String cont, String date, Exception e){
            Level = level;
            Author = aut;
            Content = cont;
            Date = date;
            Exception = e;
        }

        public override string ToString(){
            return String.Format("[ {0} ] - {1}" + System.Environment.NewLine + " From {2} : {3} " + System.Environment.NewLine + "{4} ", Date, Level.Name, Author, (Exception != null ? Exception.ToString() : ""), Content);
        }
    }
}
