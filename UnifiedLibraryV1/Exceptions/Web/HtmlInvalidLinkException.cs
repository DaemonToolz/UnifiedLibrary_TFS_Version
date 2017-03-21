using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Exceptions.Web{
    public class HtmlInvalidLinkException : Exception{


        public override String ToString(){
            return base.ToString();
        }

        public HtmlInvalidLinkException() : base() { }
        public HtmlInvalidLinkException(String message) : base(message) { }
        public HtmlInvalidLinkException(String message, Exception inner) : base(message, inner) { }
    }
}
