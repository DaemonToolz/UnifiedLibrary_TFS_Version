using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.ComponentModel;

// Modification from server
namespace UniversalLibraryCS.SystemManagement.Infos
{
    /// <summary>
    /// Class allowing to get system's information
    /// </summary>
    public sealed class SystemInfos
    {
        #region OS & Software specific
        public static String OSVersion() => Environment.OSVersion.ToString();
        public static String ArchitectureType() => Environment.Is64BitOperatingSystem ? "x64" : "x32";
        public static String SystemDirectory() => Environment.SystemDirectory;
        public static Int32 ProcessorCount() => Environment.ProcessorCount;
        public static String UserDomain() => Environment.UserDomainName;
        public static String UserName() => Environment.UserName;
        public static Int32 SystemPageFile() => Environment.SystemPageSize;
        public static String EnvironmentVersion => Environment.Version.ToString();

        public static List<List<String>> LogicalDrives() {
            List<List<String>> DriveInfos = new List<List<String>>();

            StringBuilder stringbuilder = new StringBuilder(string.Empty);

            foreach (var DriveInfo in DriveInfo.GetDrives()){
                DriveInfos.Add(new List<String>());
                try {
                    stringbuilder.AppendFormat("\t Drive: {0}\n\t\t VolumeLabel: " +
                        "{1}\n\t\t DriveType: {2}\n\t\t DriveFormat: {3}\n\t\t " +
                        "TotalSize: {4}\n\t\t AvailableFreeSpace: {5}\n",
                        DriveInfo.Name, DriveInfo.VolumeLabel, DriveInfo.DriveType,
                        DriveInfo.DriveFormat, DriveInfo.TotalSize, DriveInfo.AvailableFreeSpace);
                    DriveInfos.ElementAt(DriveInfos.Count - 1).Add(stringbuilder.ToString());
                    stringbuilder.Clear();
                }
                catch { throw; }
            }
            return DriveInfos;
        }


        public static long GetFileSizeOnDisk(string file){
            FileInfo info = new FileInfo(file);
            uint dummy, sectorsPerCluster, bytesPerSector;
            int result = GetDiskFreeSpaceW(info.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
            if (result == 0) throw new Win32Exception();
            uint clusterSize = sectorsPerCluster * bytesPerSector;
            uint hosize;
            uint losize = GetCompressedFileSizeW(file, out hosize);
            long size;
            size = (long)hosize << 32 | losize;
            return ((size + clusterSize - 1) / clusterSize) * clusterSize;
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
           [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
           out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
           out uint lpTotalNumberOfClusters);
        #endregion

        #region Hardware specific
        public static double[] SystemInfo(){
            var cCounter = 0;
            var rCounter = 0;

            using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true)){
                cCounter = (int)cpuCounter.NextValue();
                Thread.Sleep(1000);
                cCounter = (int)cpuCounter.NextValue();
            }

            using (var ram = new PerformanceCounter("Memory", "Available MBytes", true)){
                rCounter = (int)ram.NextValue();
                Thread.Sleep(1000);
                rCounter = (int)ram.NextValue();
            }

            var cInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            double total = cInfo.TotalPhysicalMemory / 1024 / 1024;

            var percentage = (rCounter / total) * 100;
            return new double[] { cCounter, percentage };
        }

        public string DeviceInformation(string stringIn){
            StringBuilder StringBuilder1 = new StringBuilder(string.Empty);
            ManagementClass ManagementClass1 = new ManagementClass(stringIn);
            ManagementObjectCollection ManagemenobjCol = ManagementClass1.GetInstances();
            PropertyDataCollection properties = ManagementClass1.Properties;
            foreach (ManagementObject obj in ManagemenobjCol){
                foreach (PropertyData property in properties){
                    try{
                        StringBuilder1.AppendLine(property.Name + ":  " + obj.Properties[property.Name].Value.ToString());
                    }
                    catch { throw; }
                }
                StringBuilder1.AppendLine();
            }
            return StringBuilder1.ToString();
        }
        #endregion

        public override String ToString()
        {
            return base.ToString();
        }
    }
}
