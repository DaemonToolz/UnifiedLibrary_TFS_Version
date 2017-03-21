using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.AsymetricalCrypt
{
    public interface IAsymetricalCrypt
    {
        byte[] PublicKey { get; }
        byte[] PrivateKey { get; }

        void CreateCouple();
        void AssignPrivateKey(byte[] key);
        void AssignPublicKey(byte[] key);
        byte[] Encrypt(byte[] dataClear);
        byte[] Decrypt(byte[] dataCrypted);
    }
}
