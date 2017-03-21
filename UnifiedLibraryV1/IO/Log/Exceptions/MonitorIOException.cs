using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Log.Exceptions{
    class MonitorIOException : IOException{

        public MonitorIOException(String message): base(message) { }

        public override string ToString(){
            return base.ToString();
        }

    }
}
