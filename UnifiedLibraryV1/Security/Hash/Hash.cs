using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.Hash
{
    public abstract class Hash : IHash
    {
        public abstract byte[] GetHashInBytes(object input);
        public abstract string GetHashInString(object input);

        protected string BytesToHexa(byte[] data)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
