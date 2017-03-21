using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Security.SymetricalCrypt.Exception
{
    public class WrongSizeKeyException : System.Exception
    {
        public WrongSizeKeyException(string message) : base(message)
        {
        }
    }
}
