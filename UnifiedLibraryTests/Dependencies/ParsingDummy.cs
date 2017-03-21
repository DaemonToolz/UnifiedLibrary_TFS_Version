using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryTests.Dependencies{
    [Serializable]
    public class ParsingDummy{
        public UInt32 UnsignedIntDummy;
        public Level2Dummy SublevelDummy;
        public ParsingDummy() { }
    }

    [Serializable]
    public class Level2Dummy : ISerializable{

        public Int64  SignedLongDummy;

        public String StringDummy;
        public Level2Dummy() { }


        protected Level2Dummy(SerializationInfo info, StreamingContext context){
            SignedLongDummy = info.GetInt64("SignedLongDummy");
            StringDummy = info.GetString("StringDummy");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context){
            info.AddValue("SignedLongDummy",this.SignedLongDummy);
            info.AddValue("StringDummy", this.StringDummy);
        }

        public override String ToString(){
            return SignedLongDummy + " " + StringDummy;
        }
    }
}
