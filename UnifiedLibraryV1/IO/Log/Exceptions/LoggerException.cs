using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Log.Exceptions{
    public class LoggerException : Exception{

        public LoggerException(String message): base(message) { }

        public override string ToString(){
            return base.ToString();
        }

    }
}
