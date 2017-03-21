using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Network.Monitor{
    public static class PortOverriding{
        private static SortedDictionary<IPEndPoint, Socket> LockedPorts;

        static PortOverriding(){
            LockedPorts = new SortedDictionary<IPEndPoint, Socket>();
        }

        public static void BlockPort(int portToBlock){
            if (portToBlock < 0) throw new Exception("Invalid port exception");
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, portToBlock);
            s.Bind(ep);
            LockedPorts.Add(ep, s);
        } 

        public static bool IsBusy(int port){
            IPGlobalProperties ipGP = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endpoints = ipGP.GetActiveTcpListeners();
            if (endpoints == null || endpoints.Length == 0) return false;
            foreach (var endpoint in endpoints) if (endpoint.Port == port) return true;
            return false;
        }

        public static IEnumerable<int> BusyPorts(){
            IPGlobalProperties ipGP = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endpoints = ipGP.GetActiveTcpListeners();

            if (endpoints == null || endpoints.Length == 0) yield return -1;
            else  foreach (var endpoint in endpoints) yield return endpoint.Port;
        }

        public static IEnumerable<int> BlockedPorts(){
            foreach (var endpoint in LockedPorts.Keys) yield return endpoint.Port;
        }

        public static void FreeAll(){
            foreach(var link in LockedPorts)
                link.Value.Close();

            LockedPorts.Clear();
        }

        public static void FreeOne(Int32 portToUnlock){
            try {
                IPEndPoint ep;
                LockedPorts[ep = (LockedPorts.Keys.ToList().First(obj => obj.Port.Equals(portToUnlock)))].Close();
                LockedPorts.Remove(ep);
            }
            catch {
                throw;
            }
        }

        public static void FreeRange(Int32 portBegin, Int32 Range){
            throw new NotImplementedException();
            try {
              
            }
            catch
            {
                throw;
            }
        }

        public static void LockRange(Int32 portBegin, Int32 Range){
            throw new NotImplementedException();
            try
            {

            }
            catch
            {
                throw;
            }
        }
    }
}
