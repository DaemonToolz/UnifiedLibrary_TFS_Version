using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Stream
{

    public sealed class DataReader : IDataReader
    {
        public MemoryStream Content { get; private set; }
        public uint ByteAvailable { get { return (uint)(this.Content.Length - this.Content.Position); } }

        private ReadingMode _mode;

        public DataReader(ReadingMode mode, byte[] input)
        {
            Content = new MemoryStream(input);
            this._mode = mode;
        }

        public byte ReadByte()
        {
            return (byte)this.Content.ReadByte();
        }

        public byte[] ReadBytes(uint count)
        {
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i++)
                buffer[i] = this.ReadByte();
            if (this._mode == ReadingMode.BigEndian) Array.Reverse(buffer);
            return buffer;
        }

        public int ReadInt()
        {
            return BitConverter.ToInt32(this.ReadBytes(4), 0);
        }

        public long ReadLong()
        {
            return BitConverter.ToInt64(this.ReadBytes(8), 0);
        }

        public short ReadShort()
        {
            return BitConverter.ToInt16(this.ReadBytes(2), 0);
        }

        public uint ReadUInt()
        {
            return BitConverter.ToUInt32(this.ReadBytes(4), 0);
        }

        public ulong ReadULong()
        {
            return BitConverter.ToUInt64(this.ReadBytes(8), 0);
        }

        public ushort ReadUShort()
        {
            return BitConverter.ToUInt16(this.ReadBytes(2), 0);
        }

        public bool ReadBool()
        {
            return this.ReadByte() == 1 ? true : false;
        }

        public string ReadString()
        {
            short size = this.ReadShort();
            return Encoding.UTF8.GetString(this.ReadBytes((uint)size));
        }

        public double ReadDouble()
        {
            return BitConverter.ToDouble(ReadBytes(8), 0);
        }

        public float ReadFloat()
        {
            return BitConverter.ToSingle(ReadBytes(4), 0);
        }

        public T ReadObject<T>()
        {
            return (T)this.ReadObject(typeof(T));
        }

        public List<T> ReadList<T>()
        {
            List<T> buffer = new List<T>();
            int count = this.ReadInt();
            for (int i = 0; i < count; i++)
                buffer.Add(this.ReadObject<T>());
            return buffer;
        }

        public Dictionary<T, U> ReadDictionary<T, U>()
        {
            Dictionary<T, U> buffer = new Dictionary<T, U>();
            int count = this.ReadInt();
            T key;
            U value;
            for (int i = 0; i < count; i++)
            {
                key = this.ReadObject<T>();
                value = this.ReadObject<U>();
                buffer[key] = value;
            }
            return buffer;
        }

        public DateTime ReadDateTime()
        {
            return new DateTime(this.ReadLong());
        }

        [Obsolete]
        public object ReadObject(Type type)
        {
            return Convert.ChangeType(this.ReadObjectAboutType(type), type);
        }

        private object ReadObjectAboutType(Type type)
        {
            switch (type.Name)
            {
                case "Int32": return this.ReadInt();
                case "Int16": return this.ReadShort();
                case "Int64": return this.ReadLong();
                case "UInt32": return this.ReadUInt();
                case "UInt16": return this.ReadUShort();
                case "UInt64": return this.ReadULong();
                case "String": return this.ReadString();
                case "Boolean": return this.ReadBool();
                case "Double": return this.ReadDouble();
                case "Single": return this.ReadFloat();
                case "Byte": return this.ReadByte();
                case "List`1": return this.ReadList<object>();
                case "Dictionary`2": return this.ReadDictionary<object, object>();
                case "DateTime": return this.ReadDateTime();
            }
            object o = Activator.CreateInstance(type);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Type typeProp = property.PropertyType;
                property.SetValue(o, Convert.ChangeType(this.ReadObjectAboutType(typeProp), property.PropertyType));
            }
            return Convert.ChangeType(o, type);
        }

        public void Close()
        {
            this.Content.Close();
        }
    }
}
