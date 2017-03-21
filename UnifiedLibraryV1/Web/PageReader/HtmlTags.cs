using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Web.PageReader {
    [Obsolete]
    public sealed class HtmlTags{
        private static int _ID = 0;

        public readonly String Name;
        public readonly int Value;
        
        public static readonly HtmlTags JS_SCRIPTS_OPEN  = new HtmlTags(_ID++,"<script");
        public static readonly HtmlTags JS_SCRIPTS_CLOSE = new HtmlTags(_ID++, "</script>");

        public static readonly HtmlTags CSS_STYLES_OPEN  = new HtmlTags(_ID++, "<link");
        public static readonly HtmlTags CSS_STYLES_CLOSE = new HtmlTags(_ID++, "</link>");

        public static readonly HtmlTags NO_END_TAGS      = new HtmlTags(0xFFFFFE, ">");
        public static readonly HtmlTags SHORT_END_TAGS   = new HtmlTags(0xFFFFFF, "/>");
        

        private HtmlTags(int value, String name){
            this.Name = name;
            this.Value = value;
        }

        public override String ToString(){
            return Name;
        }
    }

}
