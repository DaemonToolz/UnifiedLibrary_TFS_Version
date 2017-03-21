using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.SystemManagement.Security{
    public static class Administrator{
        private static WindowsPrincipal     principal;
        private static WindowsIdentity      identity;
        private static WindowsBuiltInRole   role;
        private static WindowsAccountType   type;

        public static WindowsPrincipal Principal{ get { return principal ?? new WindowsPrincipal(Identity); } }
        public static WindowsIdentity Identity{ get { return identity ?? WindowsIdentity.GetCurrent(); } }
        public static bool IsAdministrator(){ return principal.IsInRole(WindowsBuiltInRole.Administrator); }
    }
}
