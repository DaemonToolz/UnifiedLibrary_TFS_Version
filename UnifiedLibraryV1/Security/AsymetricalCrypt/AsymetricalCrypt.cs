using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.AsymetricalCrypt
{
    public enum UsageMode
    {
        Encrypting = 0,
        Decrypting,
        Creating
    }

    public abstract class AsymetricalCrypt : IAsymetricalCrypt
    {
        public abstract byte[] PrivateKey { get; }
        public abstract byte[] PublicKey { get; }

        public abstract void AssignPrivateKey(byte[] key);
        public abstract void AssignPublicKey(byte[] key);
        public abstract void CreateCouple();
        public abstract byte[] Decrypt(byte[] dataCrypted);
        public abstract byte[] Encrypt(byte[] dataClear);
    }
}
