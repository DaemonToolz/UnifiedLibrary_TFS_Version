using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedLibraryV1.Exceptions.Security;
using Microsoft.Win32;
using UnifiedLibraryV1.Security.Hash;

namespace UnifiedLibraryV1.SystemManagement.RegistryManager {
    public sealed class RegistryFullAccess : RegistryLimitedAccess{
        public delegate String SimpleSecurityDelegate(String OutputCode);
        public SimpleSecurityDelegate security;
        private BasicSecurity BS = new BasicSecurity(true, 2);
        private SHA512 InternalHash = new SHA512();
        
        public RegistryFullAccess():base(){
            
        }


        public new String Read(String Root, String Key){
            String fromLimited = base.Read(Root, Key);
            if (fromLimited != null) return fromLimited;

            if (!Security.Administrator.IsAdministrator()) throw new NotEnoughPrivilegeException("You cannot access this part of the registry");

            switch (Root) {
                case "HKEY_USERS":
                    if (!Verification())
                        break;
                    return (string)Registry.Users.GetValue(Key);

                case "HKEY_CURRENT_CONFIG":
                    if(!Verification())
                        break;
                    return (string)Registry.CurrentConfig.GetValue(Key);
            
                case "HKEY_CLASSES_ROOT":
                    if (!Verification())
                        break;
                    return (string)Registry.ClassesRoot.GetValue(Key);
               
            }
            return null;
        }

        public void AdditionnalSecurityInput(){
            // TODO
        }

        private bool Verification(){
            {
                String output = BS.GenerateSecurityCode();
                String _shadowCopy = (string)output.Clone();
                String input = security?.Invoke(output);
                if (input == null || security == null) throw new InputRequiredException("An input is required to access that part of the registry");
                if (!_shadowCopy.Equals(output) || !output.Equals(input)){
                    BS.IncreaseDifficulty();
                    return false;
                }
            }
            return true;
        }

        private void measureTimeElapsed(){

        }
    }
}
