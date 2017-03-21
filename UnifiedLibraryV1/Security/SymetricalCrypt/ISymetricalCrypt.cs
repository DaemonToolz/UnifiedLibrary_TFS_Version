using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.SymetricalCrypt
{
    public interface ISymetricalCrypt
    {
        byte[] Key { get; }

        void CreateKey();
        void AssignKey(byte[] key);
        bool CheckKey(byte[] key);
        byte[] Encrypt(byte[] dataClear);
        byte[] Decrypt(byte[] dataCrypted);
    }
}
