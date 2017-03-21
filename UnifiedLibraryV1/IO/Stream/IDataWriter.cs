using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Stream
{
    public enum WritingMode
    {
        LittleEndian =0,
        BigEndian
    }

    public interface IDataWriter
    {
        void Write(byte input);
        void Write(byte[] input);
        void Write(int input);
        void Write(Int64 input);
        void Write(Int16 input);
        void Write(UInt32 input);
        void Write(UInt64 input);
        void Write(UInt16 input);
        void Write(bool input);
        void Write(string input);
        void Write(double input);
        void Write(float input);
        void Write(DateTime input);
        void Write(List<object> input);
        void Write(object input);
        void Write(Dictionary<object, object> input);
    }
}
