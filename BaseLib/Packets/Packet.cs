using System;
using System.Text;
using System.IO;

namespace BaseLib.Packets
{
    public class Packet : IDisposable
    {
        private MemoryStream data;

        public Packet()
        {
            data = new MemoryStream();
        }

        public void BuildPacket()
        {
            this.Encrypt = false;
            this.Lenght = (ushort)(data.Length - 2);
        }

        /**
         * Packet Size (Offset 0) & 0x7FFF
         **/
        public ushort Lenght
        {
            get { return (ushort)(this.GetShort(0) & 0x7FFF); }
            set { this.SetShort(0, (ushort)((this.GetShort(0) & ~0x7FFF) | (value & 0x7FFF))); }
        }

        /**
         * Packet Encrypt (Offset 0) 1 BIT
         **/
        public bool Encrypt
        {
            get {
                ushort ret = this.GetShort(0);
                ret = Convert.ToUInt16(((ret >> 15) & 0x7FFF));
                return Convert.ToBoolean(ret);
            }
            set {
                ushort ret = this.GetShort(0);
                ret = Convert.ToUInt16(((ret & (~0x7FFF << 15)) | (Convert.ToUInt16(value) & 0x7FFF) << 15));
                this.SetShort(0, ret);
            }
        }

        /**
        * Packet Opcode (Offset 2) 
        **/
        public ushort Opcode
        {
            get { return this.GetShort(2); }
            set { this.SetShort(2, value); }
        }

        /*
         *  Packet READ/WRITE Functions
         */
        public byte GetByte(int position)
        {
            byte[] buf = new byte[1];
            data.Seek(position, SeekOrigin.Begin);
            data.Read(buf, 0, 1);
            return buf[0];
        }

        public void SetByte(int position, byte value)
        {
            data.Seek(position, SeekOrigin.Begin);
            data.Write(BitConverter.GetBytes(value), 0, 1);
        }

        public ushort GetShort(int position)
        {
            byte[] buf = new byte[2];
            data.Seek(position, SeekOrigin.Begin);
            data.Read(buf, 0, 2);
            return BitConverter.ToUInt16(buf, 0);
        }

        public void SetShort(int position, ushort value)
        {
            data.Seek(position, SeekOrigin.Begin);
            data.Write(BitConverter.GetBytes(value), 0, 2);
        }

        public uint GetInt(int position)
        {
            byte[] buf = new byte[4];
            data.Seek(position, SeekOrigin.Begin);
            data.Read(buf, 0, 4);
            return BitConverter.ToUInt32(buf, 0);
        }

        public void SetInt(int position, uint value)
        {
            data.Seek(position, SeekOrigin.Begin);
            data.Write(BitConverter.GetBytes(value), 0, 4);
        }

        public ulong GetLong(int position)
        {
            byte[] buf = new byte[8];
            data.Seek(position, SeekOrigin.Begin);
            data.Read(buf, 0, 8);
            return BitConverter.ToUInt64(buf, 0);
        }

        public void SetLong(int position, ulong value)
        {
            data.Seek(position, SeekOrigin.Begin);
            data.Write(BitConverter.GetBytes(value), 0, 8);
        }

        public float GetFloat(int position)
        {
            byte[] buf = new byte[4];
            data.Seek(position, SeekOrigin.Begin);
            data.Read(buf, 0, 4);
            return BitConverter.ToSingle(buf, 0);
        }

        public void SetFloat(int position, float value)
        {
            data.Seek(position, SeekOrigin.Begin);
            data.Write(BitConverter.GetBytes(value), 0, 4);
        }

        public string GetString(int position, int size)
        {
            data.Seek(position, SeekOrigin.Begin);
            byte[] strData = new byte[size];
            data.Read(strData, 0, size);
            String text = Encoding.Unicode.GetString(strData);
            String[] splits = text.Split('\0');
            return splits[0];
        }

        public bool GetBool(int position)
        {
            return Convert.ToBoolean(GetByte(position));
        }

        public void SetString(int position, string text, int size)
        {
            data.Seek(position, SeekOrigin.Begin);
            var strData = Encoding.Unicode.GetBytes(text);
            data.Write(strData, 0, strData.Length);
        }

        public string GetAsciiString(int position, int size)
        {
            data.Seek(position, SeekOrigin.Begin);
            byte[] strData = new byte[size];
            data.Read(strData, 0, size);
            return Encoding.ASCII.GetString(strData).Trim();
        }

        public void SetAsciiString(int position, string text)
        {
            data.Seek(position, SeekOrigin.Begin);
            byte[] strData = Encoding.ASCII.GetBytes(text);
            data.Write(strData, 0, strData.Length);
        }

        public void SetBool(int position, bool value)
        {
            SetByte(position, Convert.ToByte(value));
        }

        public void SetBytes(int position, byte[] buf)
        {
            data.Seek(position, SeekOrigin.Begin);
            data.Write(buf, 0, buf.Length);
        }

        public void SetBytes(int position, byte[] buf, int size)
        {
            data.Seek(position, SeekOrigin.Begin);
            byte[] outData = new byte[size];
            Buffer.BlockCopy(buf, 0, outData, 0, buf.Length);
            data.Write(outData, 0, outData.Length);
        }

        public byte[] GetBytes(int position, int size)
        {
            data.Seek(position, SeekOrigin.Begin);
            byte[] outData = new byte[size];
            data.Read(outData, 0, size);
            return outData;
        }

        /*
         *  Packet Control Functions
         */
        public byte[] Data
        {
            get
            {
                return data.ToArray();
            }
            set
            {
                data.Flush();
                data.Seek(0, SeekOrigin.Begin);
                data.Write(value, 0, value.Length);
            }
        }

        public void SetData(byte[] Src)
        {
            SetData(0, Src);
        }

        public void SetData(int position, byte[] Src)
        {
            SetData(position, Src, Src.Length);
        }

        public void SetData(int position, byte[] Src, int Len)
        {
             byte[] tdata;
             if (data.Capacity != 0)
                 tdata = this.Data;
             else
                 tdata = new byte[Len];
            Buffer.BlockCopy(Src, 0, tdata, position, Len);
            this.Data = tdata;
        }

        public void Dispose()
        {
            data.Close();
            data.Dispose();
        }
    }
}
