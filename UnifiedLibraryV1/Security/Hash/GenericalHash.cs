using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.Hash
{
    //
    public sealed class GenericalHash
    {
        public static byte[] GetHashInBytes<T>(object input) where T : Hash, new()
        {
            T hash = new T();
            return hash.GetHashInBytes(input);
        }

        public static string GetHashInString<T>(object input) where T : Hash, new()
        {
            T hash = new T();
            return hash.GetHashInString(input);
        }
    }
}
