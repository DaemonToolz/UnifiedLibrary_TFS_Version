using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace UnifiedLibraryV1.Security.Hash
{
    public sealed class MD5 : Hash
    {
        public override byte[] GetHashInBytes(object input)
        {
            using (System.Security.Cryptography.MD5 md = System.Security.Cryptography.MD5.Create())
            {
                return md.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
            }
        }

        public override string GetHashInString(object input)
        {
            return base.BytesToHexa(GetHashInBytes(input));
        }
    }
}
