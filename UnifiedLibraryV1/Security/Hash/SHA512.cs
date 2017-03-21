using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.Hash
{
    public sealed class SHA512 : Hash
    {
        public override byte[] GetHashInBytes(object input)
        {
            using (System.Security.Cryptography.SHA512 sha = System.Security.Cryptography.SHA512.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
            }
        }

        public override string GetHashInString(object input)
        {
            return base.BytesToHexa(GetHashInBytes(input));
        }
    }
}
