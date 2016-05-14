using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLib;
using BaseLib.Packets;
using BaseLib.Structs;

namespace CharServer.Packets
{
    class CU_CHARACTER_LOAD_RES : Packet
    {
        public CU_CHARACTER_LOAD_RES()
        {
            this.Opcode = (ushort)PacketOpcodes.CU_CHARACTER_LOAD_RES;
            this.ResultCode = 200;
            this.ServerID = 255;
            this.OpenCharSlots = 8;
            this.VIPCharSlots = 0;
        }

        public ushort ResultCode
        {
            get { return this.GetShort(4); }
            set { this.SetShort(4, value); }
        }

        public byte ServerID
        {
            get { return this.GetByte(6); }
            set { this.SetByte(6, value); }
        }

        public byte OpenCharSlots
        {
            get { return this.GetByte(7); }
            set { this.SetByte(7, value); }
        }

        public byte VIPCharSlots
        {
            get { return this.GetByte(8); }
            set { this.SetByte(8, value); }
        }
    }
}
