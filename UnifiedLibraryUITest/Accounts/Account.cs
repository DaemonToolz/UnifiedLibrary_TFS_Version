using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryUITest.Accounts{
    public struct Account {
        public String User { get; set; }
        public Int32  Age  { get; set; }
        public String Desc { get; set; }

        public Account(String usr, Int32 age, String desc){
            User = usr;
            Age = age;
            Desc = desc;
        }
    }
}
