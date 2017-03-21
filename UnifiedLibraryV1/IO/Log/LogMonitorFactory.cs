using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Log{
    public static class LogMonitorFactory{
        public  static readonly Int32 _MONITOR_COUNT = 6;
        public  static          Boolean  _AutoFlush = true;

        private static          Dictionary<String, LogMonitor> _Monitors;
        private static          Random IdGenerator;
        private static readonly Int32 _MaximumDispersion = 0x01A42DE;

        static LogMonitorFactory(){
            IdGenerator = new Random((Int32)(DateTime.Now.Ticks));
            _Monitors = new Dictionary<String, LogMonitor>();

            {
                Int32 pointless;
                for (var t = 0; t < 5; ++t)
                    IdGenerator.Next( pointless = IdGenerator.Next(), pointless + (int)_MaximumDispersion);
            }
        }

        public static LogMonitor CreateMonitor(){
            if (_Monitors.Count >= _MONITOR_COUNT)
                return null;

            LogMonitor newMonitor = new LogMonitor(IdGenerator.Next(_MaximumDispersion) + DateTime.Now.ToLongTimeString().GetHashCode() + ""+DateTime.Now.ToShortTimeString().GetHashCode(), _AutoFlush);
            _Monitors.Add(newMonitor.MonitorIdentifier, newMonitor);
            return newMonitor;
        }

        public static void StopMonitor(ref LogMonitor m){
            try {
                if (_Monitors.Keys.Contains(m.MonitorIdentifier)){
                    m.Stop();
                    _Monitors.Remove(m.MonitorIdentifier);
                    m = null;
                }
            } catch {
                throw;
            }
        }

        public static void StopAll(){
            foreach (var monitor in _Monitors)
                monitor.Value.Stop();

            _Monitors.Clear();
        }
    }
}
