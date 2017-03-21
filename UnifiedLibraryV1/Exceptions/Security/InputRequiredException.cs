using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Exceptions.Security{
    public class InputRequiredException : Exception{
        public override String ToString(){
            return base.ToString();
        }

        public InputRequiredException() : base() { }
        public InputRequiredException(String message) : base(message) { }
        public InputRequiredException(String message, Exception inner) : base(message, inner) { }
    }
}
