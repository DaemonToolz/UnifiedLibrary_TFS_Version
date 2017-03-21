using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.IO.Stream
{
    public sealed class DataWriter : IDataWriter
    {
        public MemoryStream Content { get; private set; }
        public byte [] Data { get { return Content.ToArray(); } }

        private WritingMode _mode;

        public DataWriter(WritingMode mode)
        {
            Content = new MemoryStream();
            this._mode = mode;
        }


        public void Write(byte input)
        {
            this.Content.WriteByte(input);
        }

        public void Write(byte[] input)
        {
            if(this._mode == WritingMode.BigEndian) Array.Reverse(input);
            foreach (byte b in input)
                this.Content.WriteByte(b);
        }

        public void Write(int input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(long input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(short input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(uint input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(ulong input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(ushort input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(bool input)
        {
            this.Write((byte)(input == true ? 1 : 0));
        }

        public void Write(double input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(string input)
        {
            if (input.Length > short.MaxValue)
                throw new Exception("String trop conséquent");
            this.Write((short)input.Length);
            this.Write(Encoding.UTF8.GetBytes(input));
        }


        public void Write(float input)
        {
            this.Write(BitConverter.GetBytes(input));
        }

        public void Write(List<object> input)
        {
            this.Write(input.Count);
            foreach (object o in input)
                this.Write(o);
        }

        public void Write(object[] input)
        {
            this.Write(input.Length);
            foreach (object o in input)
                this.Write(o);
        }

        public void Write(Dictionary<object, object> input)
        {
            this.Write(input.Count);
            foreach (object key in input.Keys)
            {
                this.Write(key);
                this.Write(input[key]);
            }
        }

        public void Write(object input)
        {
            string nameType = input.GetType().Name;
            switch (nameType)
            {
                case "Int32": this.Write((int)input); return;
                case "Int16": this.Write((short)input); return;
                case "Int64": this.Write((long)input); return;
                case "UInt32": this.Write((uint)input); return;
                case "UInt16": this.Write((ushort)input); return;
                case "UInt64": this.Write((ulong)input); return;
                case "String": this.Write((string)input); return;
                case "Boolean": this.Write((bool)input); return;
                case "Double": this.Write((double)input); return;
                case "Single": this.Write((float)input); return;
                case "Byte": this.Write((byte)input); return;
                case "List`1": this.Write((List<object>)input); return;
                case "DateTime": this.Write((DateTime)input); return;
                case "Dictionary`2": this.Write((Dictionary<object, object>)input); return;
            }
            PropertyInfo[] properties = input.GetType().GetProperties();
            foreach(PropertyInfo property in properties)
            {
                this.Write(property.GetValue(input));
            }
        }

        public void Close()
        {
            this.Content.Close();
        }

        public void Write(DateTime input)
        {
            this.Write(input.Ticks);
        }
    }
}
