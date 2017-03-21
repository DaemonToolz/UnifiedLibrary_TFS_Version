using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.SystemManagement.RegistryManager{
    class RegRoots{

        internal static readonly RegRoots ROOT = new RegRoots("HKEY_CLASSES_ROOT");
        internal static readonly RegRoots CONFIG = new RegRoots("HKEY_CURRENT_CONFIG");
        internal static readonly RegRoots USER = new RegRoots("HKEY_CURRENT_USER");
        internal static readonly RegRoots LOCAL = new RegRoots("HKEY_LOCAL_MACHINE");
        internal static readonly RegRoots USERS = new RegRoots("HKEY_USERS");
        internal static readonly RegRoots PERF = new RegRoots("HKEY_PERFORMANCE_DATA");


        public readonly String name;
        RegRoots(String name){
            this.name = name;
        }

    }

    public class RegistryLimitedAccess {
        public string Read(String StringRoot, String SubKey){
            switch (StringRoot){
                case "HKEY_LOCAL_MACHINE":
                    return (string)Registry.LocalMachine.GetValue(SubKey);
                case "HKEY_CURRENT_USER":
                    return (string)Registry.CurrentUser.GetValue(SubKey);
            }
            return null;
        }

    }
}
