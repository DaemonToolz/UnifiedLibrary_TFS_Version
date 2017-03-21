using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SharpPcap;
using SharpPcap.AirPcap;
using SharpPcap.WinPcap;
using SharpPcap.LibPcap;
using System.Threading;
using PacketDotNet;

/*
 * Non modulable pour le moment 
 * Les devices du même types écouteront le même type d'entrées
 */
 
namespace UnifiedLibraryV1.Network.Monitor{
    public class NetworkSurveillance : IDisposable{
        public static Int32 _MAX_PACKET_CACHED = 1024;

        public static NetworkSurveillance ActiveMonitor { get; private set; }
        private static String _OLD_SECONDS = "OldSec";
        private static String _OLD_U_SECONDS = "OldUSec";

        public static Boolean _Init { get; private set; }

        public Dictionary<String, AirPcapDevice>        AirPcapDevices { get; private set; }
        public Dictionary<String, WinPcapDevice>        WinPcapDevices { get; private set; }
        public Dictionary<String, LibPcapLiveDevice>    LibPcapLiveDevices { get; private set; }

        private Dictionary<String, Dictionary<String, ulong>> DelaysPerDevices;

        public List<String>  PacketReceived { get; private set; }
        private List<Packet> PacketQueue;

        private Mutex Lock;

        static NetworkSurveillance() {
            _Init = false;
        }

        public static void SendPacket(ICaptureDevice device, byte[] bytes){
            try{ device.Open(); } catch { }
            try{ device.SendPacket(bytes); }
            catch (Exception e){
                device.Close();
                throw e;
            }
            finally{ device.Close(); }
        }

        public NetworkSurveillance(){
            if (_Init) throw new Exception("One instance already exists");
            _Init = true;

            AirPcapDevices = new Dictionary<string, AirPcapDevice>();
            WinPcapDevices = new Dictionary<string, WinPcapDevice>();
            LibPcapLiveDevices = new Dictionary<string, LibPcapLiveDevice>();
            DelaysPerDevices = new Dictionary<string, Dictionary<String, ulong>>();

            PacketReceived = new List<string>();
            PacketQueue = new List<Packet>();
            // Retrieve all capture devices
            var devices = CaptureDeviceList.Instance;

            Lock = new Mutex();

            Lock.WaitOne();
                // differentiate based upon types
            foreach (ICaptureDevice dev in devices){
                
                if (dev is AirPcapDevice)
                    AirPcapDevices.Add(dev.Name, ((AirPcapDevice)dev));
                
                else if (dev is WinPcapDevice)
                    WinPcapDevices.Add(dev.Name, ((WinPcapDevice)dev));
                
                else if (dev is LibPcapLiveDevice)
                    LibPcapLiveDevices.Add(dev.Name, ((LibPcapLiveDevice)dev));
                
                DelaysPerDevices.Add(dev.Name, new Dictionary<String, ulong>());
                DelaysPerDevices[dev.Name].Add(_OLD_SECONDS, 0);
                DelaysPerDevices[dev.Name].Add(_OLD_U_SECONDS, 0);

            }

            Lock.ReleaseMutex();
            ActiveMonitor = this;
        }

        public void InitMonitor(String AirFilter = "", String WinFilter = "", String LibFilter = ""){
            StopMonitor();
            
            int readTimeoutMilliseconds = 500;
            ICaptureDevice device;
            
            if (AirFilter == null || AirFilter.Trim().Count() == 0) AirFilter = NetworkFilters.Combine(NetworkFilters.TCP, NetworkFilters.ICMP);
            if (WinFilter == null || WinFilter.Trim().Count() == 0) WinFilter = NetworkFilters.Combine(NetworkFilters.TCP, NetworkFilters.ICMP);
            if (LibFilter == null || LibFilter.Trim().Count() == 0) LibFilter = NetworkFilters.Combine(NetworkFilters.TCP, NetworkFilters.ICMP);

            foreach (var airDevice in AirPcapDevices){
                device = airDevice.Value;
                device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);

                device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                device.Filter = AirFilter;
                device.StartCapture();
            }

            foreach (var winDevice in WinPcapDevices){
                device = winDevice.Value;  
                device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);
                device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                device.Filter = WinFilter;
                device.StartCapture();
            }

            foreach (var libDevice in LibPcapLiveDevices){
                device = libDevice.Value;
                device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);
                device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                device.Filter = LibFilter;
                device.StartCapture();
            }
        }

        public void StopMonitor(){
            AirPcapDevices.Values.ToList().ForEach(obj => obj.StopCapture());
            WinPcapDevices.Values.ToList().ForEach(obj => obj.StopCapture());
            LibPcapLiveDevices.Values.ToList().ForEach(obj => obj.StopCapture());
        }

        public void CloseMonitor(){
            StopMonitor();

            AirPcapDevices.Values.ToList().ForEach(obj => obj.Close());
            WinPcapDevices.Values.ToList().ForEach(obj => obj.Close());
            LibPcapLiveDevices.Values.ToList().ForEach(obj => obj.Close());

            AirPcapDevices.Clear();
            WinPcapDevices.Clear();
            LibPcapLiveDevices.Clear();

            ActiveMonitor = null;
            _Init = false;
        }

        public void ChangeFilters(String AirFilter = "", String WinFilter = "", String LibFilter = ""){
            StopMonitor();

            // Default TCP/IP
            if (AirFilter == null || AirFilter.Trim().Count() == 0) AirFilter = NetworkFilters.Combine(NetworkFilters.TCP, NetworkFilters.ICMP);
            if (WinFilter == null || WinFilter.Trim().Count() == 0) WinFilter = NetworkFilters.Combine(NetworkFilters.TCP, NetworkFilters.ICMP);
            if (LibFilter == null || LibFilter.Trim().Count() == 0) LibFilter = NetworkFilters.Combine(NetworkFilters.TCP, NetworkFilters.ICMP);

            foreach (var airDevice in AirPcapDevices){
                airDevice.Value.Filter = AirFilter;
                airDevice.Value.StartCapture();
            }

            foreach (var winDevice in WinPcapDevices){
                winDevice.Value.Filter = WinFilter;
                winDevice.Value.StartCapture();
            }

            foreach (var libDevice in LibPcapLiveDevices){
                libDevice.Value.Filter = LibFilter;
                libDevice.Value.StartCapture();
            }
        }

        public IEnumerable<ICaptureStatistics> RetrieveStatistics(Type deviceType){
            if (deviceType.Equals(typeof(AirPcapDevice))) foreach (var device in AirPcapDevices) yield return device.Value.Statistics;
            if (deviceType.Equals(typeof(WinPcapDevice))) foreach (var device in WinPcapDevices) yield return device.Value.Statistics;
            if (deviceType.Equals(typeof(LibPcapLiveDevice))) foreach (var device in LibPcapLiveDevices) yield return device.Value.Statistics;
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs packet){
            try
            {
                Packet nextPacket = Packet.ParsePacket(packet.Packet.LinkLayerType, packet.Packet.Data);

                DateTime time = packet.Packet.Timeval.Date;
                int len = packet.Packet.Data.Length;

                String stamp = time.Hour + ":" + time.Minute + ":" + time.Second + ":" + time.Millisecond + " - " + len + " | " + nextPacket.BytesHighPerformance.ToString() + " | | | ";


                if (nextPacket is UdpPacket){

                    UdpPacket udp = (UdpPacket)nextPacket;
                    ushort srcPort = udp.SourcePort;
                    ushort dstPort = udp.DestinationPort;
                    String header = BitConverter.ToString(udp.Header);
                    String data = BitConverter.ToString(udp.PayloadData);

                    stamp +=  " UDP : " + header + " | " + data + " | FROM " + srcPort + " TO " + dstPort;
                }
                else if (nextPacket is TcpPacket){
                    TcpPacket tcp = (TcpPacket)nextPacket;
                    ushort srcPort = tcp.SourcePort;
                    ushort dstPort = tcp.DestinationPort;
                    String header = BitConverter.ToString(tcp.Header);
                    String data = BitConverter.ToString(tcp.PayloadData);
                    String options = BitConverter.ToString(tcp.Options);
                    String colOpt = tcp.OptionsCollection.ToString();

                    stamp += " TCP : " + header + " | " + data + " | " + options + " | FROM " + srcPort + " TO " + dstPort;
                }

                Lock.WaitOne();
                if (PacketReceived.Count > _MAX_PACKET_CACHED)
                    PacketReceived.Clear();

                if (PacketQueue.Count > _MAX_PACKET_CACHED)
                    PacketQueue.Clear();

                PacketQueue.Add(nextPacket);
                PacketReceived.Add(stamp);
                Lock.ReleaseMutex();
            }
            catch {
                Lock.ReleaseMutex();
                throw;
            }
        }

        [Obsolete]
        private void device_tcpipOnPacketArrival(object sender, Packet packet){
            try {
                String stamp = "";

                if (packet is UdpPacket) {

                    UdpPacket udp = (UdpPacket)packet;
                    int len = udp.Length;
                    ushort srcPort = udp.SourcePort;
                    ushort dstPort = udp.DestinationPort;
                    String header = BitConverter.ToString(udp.Header);
                    String data = BitConverter.ToString(udp.PayloadData);

                    stamp = DateTime.Now.ToLongTimeString() + " - UDP : " + header + " | " + data + " | FROM " + srcPort + " TO " + dstPort;
                }
                else if (packet is TcpPacket){
                    TcpPacket tcp = (TcpPacket)packet;
                    int len = tcp.BytesHighPerformance.Length;
                    ushort srcPort = tcp.SourcePort;
                    ushort dstPort = tcp.DestinationPort;
                    String header = BitConverter.ToString(tcp.Header);
                    String data = BitConverter.ToString(tcp.PayloadData);
                    String options = BitConverter.ToString(tcp.Options);
                    String colOpt = tcp.OptionsCollection.ToString();

                    stamp = DateTime.Now.ToLongTimeString() + " - TCP : " + header + " | " + data + " | " + options + " | FROM " + srcPort + " TO " + dstPort;

                }


                Lock.WaitOne();
                if (PacketReceived.Count > _MAX_PACKET_CACHED)
                    PacketReceived.Clear();

                if (PacketQueue.Count > _MAX_PACKET_CACHED)
                    PacketQueue.Clear();

                PacketQueue.Add(packet);
                PacketReceived.Add(stamp);
                Lock.ReleaseMutex();
            }
            catch {
                Lock.ReleaseMutex();
                throw;
            }

        }

        [Obsolete]
        private void device_OnPcapStatistics( object sender, StatisticsModeEventArgs statistics){

            ulong delay = (statistics.Statistics.Timeval.Seconds - DelaysPerDevices[statistics.Device.Name][_OLD_SECONDS]) * 1000000 - DelaysPerDevices[statistics.Device.Name][_OLD_U_SECONDS] + statistics.Statistics.Timeval.MicroSeconds;

            long bps = (statistics.Statistics.RecievedBytes * 8 * 1000000) / (long)delay;
 
            // Get the number of Packets per second
            long pps = (statistics.Statistics.RecievedPackets * 1000000) / (long)delay;


            // store current timestamp
            DelaysPerDevices[statistics.Device.Name][_OLD_SECONDS] = statistics.Statistics.Timeval.Seconds;
            DelaysPerDevices[statistics.Device.Name][_OLD_U_SECONDS] = statistics.Statistics.Timeval.MicroSeconds;
        }

        public void Dispose(){
            CloseMonitor();
        }
    }
}
