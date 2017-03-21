using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Exceptions.Web{
    public class HtmlEmptyContent : Exception{


        public override String ToString(){
            return base.ToString();
        }

        public HtmlEmptyContent() : base() { }
        public HtmlEmptyContent(String message) : base(message) { }
        public HtmlEmptyContent(String message, Exception inner) : base(message, inner) { }
    }
}
