using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedLibraryV1.Network.Monitor;

namespace UnifiedLibraryV1.SystemManagement.Compression{
    public class Monitor{
        public MessageLevel Level;
        public bool UseOutPut { get; set; }
        public delegate void ProgressInformationDelegate(string sMessage);
        public delegate void ProgressVerboseDelegate(double percentage, string message);
        public delegate void ProgressSimpleDelegate(double percentage);

        public ProgressInformationDelegate Information;
        public ProgressVerboseDelegate Verbose;
        public ProgressSimpleDelegate Simple;

        protected void Display(double percentage, string msg) {
            if (Level == MessageLevel.None || !UseOutPut) return;
            switch (Level){
                case MessageLevel.Simple:
                    Simple?.Invoke(percentage);
                    goto case MessageLevel.Information;
                case MessageLevel.Information:
                    Information?.Invoke(msg);
                    goto case MessageLevel.Verbose;
                case MessageLevel.Verbose:
                    Verbose?.Invoke(percentage, msg);
                    break;
            }
        }
    }
}
