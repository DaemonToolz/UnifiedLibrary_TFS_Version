using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifiedLibraryV1.Network.Monitor;
using PacketDotNet;
using SharpPcap;

namespace UnifiedLibraryTests{
    [TestClass]
    public class TestSniffer{

        [TestMethod]
        [ExpectedException(typeof(PcapException))]
        public void TestNetworkInitialization(){
            
            NetworkSurveillance nsk = new NetworkSurveillance();
            nsk.InitMonitor(null,null,null);

            Assert.IsNotNull(nsk.AirPcapDevices);
            foreach (var device in nsk.AirPcapDevices)
                Console.WriteLine(device.Value.ToString());

            Assert.IsNotNull(nsk.WinPcapDevices);

            foreach (var device in nsk.WinPcapDevices)
                Console.WriteLine(device.Value.ToString());

            Assert.IsNotNull(nsk.LibPcapLiveDevices);

            foreach (var device in nsk.LibPcapLiveDevices)
                Console.WriteLine(device.Value.ToString());

            /*
            ushort tcpSourcePort = 123;
            ushort tcpDestinationPort = 321;
            var tcpPacket = new TcpPacket(tcpSourcePort, tcpDestinationPort);

            var ipSourceAddress = System.Net.IPAddress.Parse("192.168.1.1");
            var ipDestinationAddress = System.Net.IPAddress.Parse("192.168.1.2");
            var ipPacket = new IPv4Packet(ipSourceAddress, ipDestinationAddress);

            var sourceHwAddress = "90-90-90-90-90-90";
            var ethernetSourceHwAddress = System.Net.NetworkInformation.PhysicalAddress.Parse(sourceHwAddress);
            var destinationHwAddress = "80-80-80-80-80-80";
            var ethernetDestinationHwAddress = System.Net.NetworkInformation.PhysicalAddress.Parse(destinationHwAddress);

            // NOTE: using EthernetPacketType.None to illustrate that the Ethernet
            //       protocol type is updated based on the packet payload that is
            //       assigned to that particular Ethernet packet
            var ethernetPacket = new EthernetPacket(ethernetSourceHwAddress,
                ethernetDestinationHwAddress,
                EthernetPacketType.None);

            // Now stitch all of the packets together
            ipPacket.PayloadPacket = tcpPacket;
            ethernetPacket.PayloadPacket = ipPacket;

            // and print out the packet to see that it looks just like we wanted it to
            Console.WriteLine(ethernetPacket.ToString());

            // to retrieve the bytes that represent this newly created EthernetPacket use the Bytes property
            byte[] packetBytes = ethernetPacket.Bytes;
            */
            nsk.CloseMonitor();
            
        }
    }
}
