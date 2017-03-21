using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Stream
{
    public enum ReadingMode
    {
        LittleEndian =0,
        BigEndian
    }

    public interface IDataReader
    {
        byte ReadByte();
        byte[] ReadBytes(uint count);
        int ReadInt();
        long ReadLong();
        short ReadShort();
        uint ReadUInt();
        ulong ReadULong();
        ushort ReadUShort();
        bool ReadBool();
        String ReadString();
        double ReadDouble();
        float ReadFloat();
        DateTime ReadDateTime();
        object ReadObject(Type type);
        T ReadObject<T>();
        List<T> ReadList<T>();
        Dictionary<T, U> ReadDictionary<T, U>();
    }
}
