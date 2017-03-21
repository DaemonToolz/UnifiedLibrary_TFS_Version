using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Network.Monitor{
    public class ArchiveExtensions {
        private static int _ID = 0;

        public readonly String Name;
        public readonly int Value;

        // public static readonly ArchiveExtensions ZIP        = new ArchiveExtensions(_ID++, ".zip");
        // public static readonly ArchiveExtensions RAR        = new ArchiveExtensions(_ID++, ".rar");
        public static readonly ArchiveExtensions GZIP       = new ArchiveExtensions(_ID++, ".gz");

        private ArchiveExtensions(int value, String name){
            this.Name = name;
            this.Value = value;
        }

        public override String ToString(){
            return Name;
        }

    }
}
