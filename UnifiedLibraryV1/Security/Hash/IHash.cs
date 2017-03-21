using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.Hash
{
    public interface IHash
    {
        string GetHashInString(object input);
        byte[] GetHashInBytes(object input);
    }
}
