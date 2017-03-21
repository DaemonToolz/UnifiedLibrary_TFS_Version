using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.SymetricalCrypt
{
    public abstract class SymetricalCrypt : ISymetricalCrypt
    {
        public abstract byte[] Key { get; }

        public abstract void CreateKey();
        public abstract void AssignKey(byte[] key);
        public abstract bool CheckKey(byte[] key);
        public abstract byte[] Encrypt(byte[] dataClear);
        public abstract byte[] Decrypt(byte[] dataCrypted);
    }
}
