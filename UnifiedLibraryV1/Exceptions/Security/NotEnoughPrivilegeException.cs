using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Exceptions.Security{
    public class NotEnoughPrivilegeException : Exception{
        public override String ToString(){
            return base.ToString();
        }

        public NotEnoughPrivilegeException() : base() { }
        public NotEnoughPrivilegeException(String message) : base(message) { }
        public NotEnoughPrivilegeException(String message, Exception inner) : base(message, inner) { }
    }
}
