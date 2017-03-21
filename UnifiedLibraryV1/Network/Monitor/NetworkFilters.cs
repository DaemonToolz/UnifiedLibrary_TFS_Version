using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Network.Monitor{
    public class NetworkFilters {
        private static int _ID = 0;

        public readonly String Name;
        public readonly int Value;

        public static readonly NetworkFilters ETH       = new NetworkFilters(_ID++, "ethernet");
        public static readonly NetworkFilters TCP       = new NetworkFilters(_ID++, "tcp");
        public static readonly NetworkFilters UDP       = new NetworkFilters(_ID++, "udp");
        public static readonly NetworkFilters SSL       = new NetworkFilters(_ID++, "ssl");
        public static readonly NetworkFilters ARP       = new NetworkFilters(_ID++, "arp");
        public static readonly NetworkFilters ICMP      = new NetworkFilters(_ID++, "icmp");
        public static readonly NetworkFilters IGMPv2    = new NetworkFilters(_ID++, "igmpv2");
        public static readonly NetworkFilters PPPoE     = new NetworkFilters(_ID++, "pppoe");
        public static readonly NetworkFilters PTP       = new NetworkFilters(_ID++, "ptp");
        public static readonly NetworkFilters LLDP      = new NetworkFilters(_ID++, "lldp");

        private NetworkFilters(int value, String name){
            this.Name = name;
            this.Value = value;
        }

        public override String ToString(){
            return Name;
        }

        public static String Combine(params NetworkFilters[] filters){
            String final = "";
            foreach (var f in filters)
                final += f.Name + (!f.Equals(filters.Last()) ? " and " : "");
            return final;
        }
    }
}
