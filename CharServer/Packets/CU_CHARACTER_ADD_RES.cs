using BaseLib.Packets;
using BaseLib.Structs;
using System;

namespace CharServer.Packets
{
    class CU_CHARACTER_ADD_RES : Packet
    {
        public CU_CHARACTER_ADD_RES()
        {
            Opcode = (ushort)PacketOpcodes.CU_CHARACTER_ADD_RES;
            ResultCode = (ushort)ResultCodes.CHARACTER_SUCCESS;
        }

        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }

        public uint CharID
        {
            get { return this.GetInt(6); }
            set { this.SetInt(6, value); }
        }

        public string Name
        {
            get { return this.GetString(10, 34); }
            set { this.SetString(10, value, 34); }
        }

        public byte Race
        {
            get { return this.GetByte(44); }
            set { this.SetByte(44, value); }
        }

        public byte Class
        {
            get { return this.GetByte(45); }
            set { this.SetByte(45, value); }
        }

        public bool IsAdult
        {
            get { return Convert.ToBoolean(this.GetByte(46)); }
            set { this.SetByte(46, Convert.ToByte(value)); }
        }
    }
}
